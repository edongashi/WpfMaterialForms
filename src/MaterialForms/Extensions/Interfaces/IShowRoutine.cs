using System.Threading.Tasks;
using MaterialForms.Extensions.Models;
using MaterialForms.Wpf.Controls;

namespace MaterialForms.Extensions.Interfaces
{
    public interface IShowRoutine<T>
    {
        DynamicForm Form { get; set; }
        Task<DialogResult<T>> Show();
    }
}