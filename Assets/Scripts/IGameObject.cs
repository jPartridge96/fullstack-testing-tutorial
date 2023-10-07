public interface IGameObject
{
    string name { get; set; }
    ITransform transform { get; }
}