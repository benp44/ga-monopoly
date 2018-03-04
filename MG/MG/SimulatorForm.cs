using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG
{
    public partial class SimulatorForm : Form
    {
        private Simulation _environment;
        private List<BackgroundWorker> _workers;
        private Dictionary<string, List<Tuple<int, double, Color>>> _graphData;

        private volatile bool _continueRunning;
        private volatile int _workersRunning;
        private int _lastGamesPlayedCount;
        private int _averageGeneration;
        private int _graphXBase;
        private DateTime _startTime;
        private List<RowItem> _gridRows;

        private Dictionary<Color, Pen> _pens;
        private Dictionary<Color, Brush> _brushes;

        int _graphXZoom = 5;

        private int _workersIdle
        {
            get
            {
                return _workers.Count - _workersRunning;
            }
        }

        public SimulatorForm()
        {
            _graphData = new Dictionary<string, List<Tuple<int, double, Color>>>();
            _pens = new Dictionary<Color, Pen>();
            _brushes = new Dictionary<Color, Brush>();

            InitializeComponent();

            _gridRows = new List<RowItem>();
            listViewData.SetObjects(_gridRows);

            var items = Enum.GetValues(typeof(GeneType));
            Array.Reverse(items);
            foreach (var item in items)
            {
                var radio = new RadioButton()
                {
                    Text = item.ToString(),
                    Tag = (int)item,
                    Padding = new Padding(0),
                    Margin = new Padding(0),
                    Checked = (int)item == -1 ? true : false
                };

                radio.CheckedChanged += Radio_CheckedChanged;

                panelFilters.Controls.Add(radio);
            }

            _workers = new List<BackgroundWorker>();
            _environment = new Simulation(0, 0.5d);

            _workersRunning = 0;
            _lastGamesPlayedCount = 0;
            _continueRunning = false;
            _averageGeneration = 0;
            _graphXBase = 0;
            _startTime = DateTime.Now;

            timerRefresh.Tick += TimerRefresh_Tick;

            listViewData.SmallImageList = new ImageList();

            AssetReference.AssetList.Select(x => x.AssetColor).Distinct().ToList().ForEach(c =>
            {
                AddToImageList(listViewData.SmallImageList, c);
                _pens.Add(c, new Pen(c));
                _brushes.Add(c, new SolidBrush(c));
            });

            Reset();
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            _graphData.Clear();
        }

        private void GeneticsForm_Load(object sender, EventArgs e)
        {
            timerRefresh.Start();
        }

        private void GeneticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void AddToImageList(ImageList imageList, Color color)
        {
            imageList.Images.Add(CreateImage(color));
            imageList.Images.SetKeyName(imageList.Images.Count - 1, color.Name);
        }

        private Image CreateImage(Color color)
        {
            var bmp = new Bitmap(16, 16);

            using (var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(new SolidBrush(color), 0, 0, 16, 16);
            }

            return (Image)bmp;
        }

        private void Worker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            _workersRunning--;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _workersRunning++;

            while (_continueRunning)
            {
                _environment.PlayAndBreed();
            }

            e.Result = true;
        }

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                RefreshInfo();
            });
        }

        private void Menu_Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void Menu_Reset(object sender, EventArgs e)
        {
            Reset();
        }

        private void Menu_Start(object sender, EventArgs e)
        {
            Start();
        }

        private void Menu_Stop(object sender, EventArgs e)
        {
            Stop();
        }

        private void RefreshInfo()
        {
            MenuItemReset.Enabled = !_continueRunning;
            MenuItemStart.Enabled = !_continueRunning;
            MenuItemStop.Enabled = _continueRunning;

            RefreshCounters();
            RefreshData();
        }

        private void Reset()
        {
            var dlg = new GetParametersForm();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _environment = new Simulation(dlg.InitialPopulation, 0.25d);

                _workers.Clear();
                _graphData.Clear();

                for (int i = 0; i < dlg.WorkerCount; i++)
                {
                    var worker = new BackgroundWorker();
                    worker.DoWork += Worker_DoWork;
                    worker.RunWorkerCompleted += Worker_Complete;
                    _workers.Add(worker);
                }
            }
        }

        private void Start()
        {
            _continueRunning = true;
            _startTime = DateTime.Now;

            _workers.ForEach(x => x.RunWorkerAsync());

            while (_workersRunning < _workers.Count)
            {
                Thread.Sleep(20);
                Application.DoEvents();
            }
        }

        private void Stop()
        {
            _continueRunning = false;

            while (_workersRunning > 0)
            {
                Thread.Sleep(20);
                Application.DoEvents();
            }
        }

        private void RefreshCounters()
        {
            if (_continueRunning)
            {
                textBoxTimeRunning.Text = (DateTime.Now - _startTime).ToString(@"hh\:mm\:ss");
            }
            else
            {
                textBoxTimeRunning.Text = "00:00:00";
            }

            textBoxWorkersIdle.Text = _workersIdle.ToString();
            textBoxWorkersRunning.Text = _workersRunning.ToString();

            textBoxPopulation.Text = _environment.Population.ToString();
            textBoxCumulative.Text = _environment.CumulativePopulation.ToString();
            textBoxGamesPlayed.Text = _environment.GamesPlayed.ToString();
            textBoxRate.Text = ((float)(_environment.GamesPlayed - _lastGamesPlayedCount) / (((float)timerRefresh.Interval) / 1000f)).ToString("N1");

            _lastGamesPlayedCount = _environment.GamesPlayed;
        }

        private void RefreshData()
        {
            var results = new Dictionary<GeneIdentifier, Pair<double, int>>();

            lock (_environment.AllPlayers)
            {
                _environment.AllPlayers.ForEach(player =>
                {
                    player.Genetics.GetGenes().ToList().ForEach(gene =>
                    {
                        if (results.ContainsKey(gene.Key))
                        {
                            results[gene.Key].First += gene.Value;
                            results[gene.Key].Second++;
                        }
                        else
                        {
                            results.Add(gene.Key, new Pair<double, int> { First = gene.Value, Second = 1 });
                        }
                    });
                });

                if (_environment.AllPlayers.Count > 0)
                {
                    _averageGeneration = (int)_environment.AllPlayers.Average(x => x.Genetics.Generation);
                }
                else
                {
                    _averageGeneration = 0;
                }

                textBoxAvGeneration.Text = _averageGeneration.ToString();
            }

            foreach (var item in results)
            {
                if (GetFilter() == item.Key.GeneType || GetFilter() == GeneType.All)
                {
                    var existingRow = _gridRows.Where(x => x.GeneType == item.Key.GeneType.ToString() && x.AssetName == item.Key.Asset.Name).FirstOrDefault();
                    if (existingRow != null)
                    {
                        existingRow.AveragePreference = (item.Value.First / (double)item.Value.Second);
                        existingRow.Prevalence = item.Value.Second;
                    }
                    else
                    {
                        var rowItem = new RowItem();
                        rowItem.GeneType = item.Key.GeneType.ToString();
                        rowItem.AssetName = item.Key.Asset.Name;
                        rowItem.AveragePreference = (item.Value.First / (double)item.Value.Second);
                        rowItem.Prevalence = item.Value.Second;
                        rowItem.Order = item.Key.Asset.Order;
                        rowItem.RowColor = item.Key.Asset.AssetColor;
                        _gridRows.Add(rowItem);
                    }
                }
                else
                {
                    var existingRow = _gridRows.Where(x => x.GeneType == item.Key.GeneType.ToString() && x.AssetName == item.Key.Asset.Name).FirstOrDefault();
                    if (existingRow != null)
                    {
                        _gridRows.Remove(existingRow);
                    }
                }
            }

            olvColumnImage.ImageGetter = delegate (object rowObject)
            {
                return ((RowItem)rowObject).RowColor.Name;
            };

            listViewData.BuildList();

            if (_continueRunning)
            {
                DrawGraph();
            }
        }

        private void DrawGraph()
        {
            var seconds = (int)(DateTime.Now - _startTime).TotalSeconds;

            foreach (var item in listViewData.Objects)
            {
                var rowItem = (RowItem)item;

                if (GetFilter().ToString() == rowItem.GeneType || GetFilter() == GeneType.All)
                {
                    _graphData.CreateOrAddToList(rowItem.AssetName, new Tuple<int, double, Color>(seconds, rowItem.AveragePreference, rowItem.RowColor));
                }
            }

            pictureBoxGraph.Invalidate();
        }

        private void pictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            var seconds = (int)(DateTime.Now - _startTime).TotalSeconds;

            if (((seconds - _graphXBase) / _graphXZoom) > pictureBoxGraph.Width)
            {
                _graphXBase = _graphXBase + pictureBoxGraph.Width;
            }

            for (int i = 1; i < 10; i++)
            {
                var yVal = ((pictureBoxGraph.Height / 10) + 10) * i;
                e.Graphics.DrawLine(Pens.LightGray, 0, yVal, pictureBoxGraph.Width, yVal);
            }

            foreach (var graphPoint in _graphData)
            {
                int? lastY = null;

                for (int i = 0; i < graphPoint.Value.Count; i++)
                {
                    if (i > 0)
                    {
                        if (graphPoint.Value[i - 1].Item1 < graphPoint.Value[i].Item1)
                        {
                            e.Graphics.DrawLine(_pens[graphPoint.Value[i].Item3],
                                                new Point((graphPoint.Value[i - 1].Item1 - _graphXBase) / _graphXZoom, TranslateToGraph(graphPoint.Value[i - 1].Item2)),
                                                new Point((graphPoint.Value[i].Item1 - _graphXBase) / _graphXZoom, TranslateToGraph(graphPoint.Value[i].Item2)));

                            lastY = TranslateToGraph(graphPoint.Value[i].Item2);
                        }
                    }
                }

                if (lastY.HasValue)
                {
                    e.Graphics.DrawString(graphPoint.Key, this.Font, _brushes[graphPoint.Value[0].Item3], (float)((seconds - _graphXBase) / _graphXZoom), (float)lastY);
                }
            }

            e.Graphics.DrawLine(Pens.DarkGray, (seconds - _graphXBase) / _graphXZoom, 0, (seconds - _graphXBase) / _graphXZoom, pictureBoxGraph.Height);
        }

        private int TranslateToGraph(double yValue)
        {
            return pictureBoxGraph.Height - (int)(yValue * pictureBoxGraph.Height);
        }

        private GeneType GetFilter()
        {
            foreach (Control item in panelFilters.Controls)
            {
                if (item is RadioButton)
                {
                    if (((RadioButton)item).Checked)
                    {
                        return (GeneType)item.Tag;
                    }
                }
            }

            return GeneType.All;
        }
    }

    class RowItem
    {
        public string AssetName { get; set; }
        public double AveragePreference { get; set; }
        public int Prevalence { get; set; }
        public int Order { get; set; }
        public Color RowColor { get; set; }
        public string GeneType { get; internal set; }
    }
}
