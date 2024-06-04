public interface IJsonHandler<T>
{
    List<T> LoadAll();
    void WriteAll(List<T> data);
}