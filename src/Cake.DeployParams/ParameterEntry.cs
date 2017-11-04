namespace Cake.DeployParams
{
  public class ParameterEntry
  {
    public ParameterEntry(string match, string scope, string kind)
    {
      this.Match = match;
      this.Scope = scope;
      this.Kind = kind;
    }

    public string Kind { get; set; }

    public string Match { get; set; }

    public string Scope { get; set; }
  }
}
