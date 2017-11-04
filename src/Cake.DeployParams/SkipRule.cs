namespace Cake.DeployParams
{
    public class SkipRule
    {
        public SkipRule()
        {
        }

        public SkipRule(string name, string skipAction, string objectName, string absolutePath, string xpath = null)
        {
            this.Name = name;
            this.SkipAction = skipAction;
            this.ObjectName = objectName;
            this.AbsolutePath = absolutePath;
            this.XPath = xpath;
        }

        public string AbsolutePath { get; set; }

        public string Name { get; set; }

        public string ObjectName { get; set; }

        public string SkipAction { get; set; }

        public string XPath { get; set; }
    }
}
