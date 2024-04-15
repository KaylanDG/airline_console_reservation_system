public class PlaneLogic
{
    private List<PlaneModel> _planes;

    public PlaneLogic()
    {
        _planes = PlaneAccess.LoadAllPlanes();
    }

    public List<PlaneModel> GetPlanes()
    {
        return _planes;
    }
}
