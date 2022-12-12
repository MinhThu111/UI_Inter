using DemoUI.Models;

namespace DemoUI.Services
{
    public interface IS_Student
    {
        List<M_Student> getAll();
        bool Create(M_Student model);
    }
}
