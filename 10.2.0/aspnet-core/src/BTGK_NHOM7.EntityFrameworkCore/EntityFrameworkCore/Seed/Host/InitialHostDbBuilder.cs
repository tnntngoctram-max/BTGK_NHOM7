namespace BTGK_NHOM7.EntityFrameworkCore.Seed.Host;

public class InitialHostDbBuilder
{
    private readonly BTGK_NHOM7DbContext _context;

    public InitialHostDbBuilder(BTGK_NHOM7DbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();
        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context).Create();

        _context.SaveChanges();
    }
}
