namespace MG
{
    public class AssetSquare : Square
    {
        public Asset Asset { get; set; }

        public AssetSquare(Asset asset, int order)
            : base (order)
        {
            Asset = asset;
        }

        public override string Name
        {
            get { return Asset.Name; }
        }

        public override System.Drawing.Color DisplayColor
        {
            get { return Asset.AssetColor; }
        }
    }
}
