using System;
using System.Collections.Generic;

namespace MG
{
    public enum CrossProcess
    {
        RandomSwap,
        Average,
    }

    public enum GeneType : int
    {
        All = -1,

        PropertyValue = 0,
        ImprovementValue = 1
    }

    public class GeneticCode
    {
        private Dictionary<GeneIdentifier, double> Genes;
        public int Generation { get; private set; }
        public CrossProcess CrossingProcess { get; private set; }

        public GeneticCode(int generation = 1)
        {
            Genes = new Dictionary<GeneIdentifier, double>();
            Generation = generation;
            CrossingProcess = CrossProcess.RandomSwap;
        }

        public double GetGeneExpression(GeneType action, Asset genotype)
        {
            var identifier = new GeneIdentifier(genotype, action);

            if (Genes.ContainsKey(identifier) == false)
            {
                Genes[identifier] = Utils.GetRandomDouble();
            }

            return Genes[identifier];
        }

        public void SetGeneExpression(GeneType action, Asset genotype, double value)
        {
            Genes.CreateOrSet(new GeneIdentifier(genotype, action), value);
        }

        public Dictionary<GeneIdentifier, double> GetGenes()
        {
            return Genes;
        }

        public GeneticCode Cross(GeneticCode partner, double mutatationRate = 0.5d)
        {
            var offspringGenes = new GeneticCode(Math.Max(this.Generation, partner.Generation) + 1);

            foreach (var genotype in this.Genes.Keys)
            {
                if (partner.Genes.ContainsKey(genotype))
                {
                    if (CrossingProcess == CrossProcess.Average)
                    {
                        offspringGenes.Genes[genotype] = (this.Genes[genotype] + partner.Genes[genotype]) / 2f;
                    }
                    else
                    {
                        var rnd = Utils.GetRandomNumber(0, 2);
                        offspringGenes.Genes[genotype] = rnd == 0 ? this.Genes[genotype] : partner.Genes[genotype];
                    }
                }
                else
                {
                    offspringGenes.Genes[genotype] = this.Genes[genotype];
                }
            }

            foreach (var genotype in partner.Genes.Keys)
            {
                if (this.Genes.ContainsKey(genotype))
                {
                    if (offspringGenes.Genes.Keys.DoesNotContain(genotype))
                    {
                        if (CrossingProcess == CrossProcess.Average)
                        {
                            offspringGenes.Genes[genotype] = (this.Genes[genotype] + partner.Genes[genotype]) / 2f;
                        }
                        else
                        {
                            var rnd = Utils.GetRandomNumber(0, 2);
                            offspringGenes.Genes[genotype] = rnd == 0 ? this.Genes[genotype] : partner.Genes[genotype];
                        }
                    }
                }
                else
                {
                    offspringGenes.Genes[genotype] = partner.Genes[genotype];
                }
            }

            if (offspringGenes.Genes.Count > 0 && (Utils.GetRandomDouble() > mutatationRate))
            {
                var rnd = Utils.GetRandomNumber(0, offspringGenes.Genes.Count);
                var list = new List<GeneIdentifier>(offspringGenes.Genes.Keys);
                offspringGenes.Genes[list[rnd]] = Utils.GetRandomDouble();
            }

            return offspringGenes;
        }
    }
}