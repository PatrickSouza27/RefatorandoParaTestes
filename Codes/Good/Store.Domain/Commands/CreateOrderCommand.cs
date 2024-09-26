using System.Windows.Input;
using Flunt.Notifications;
using Store.Domain.Commands.Interfaces;

//tomar cuidado na hora de usar o using ICommand, pois ele pode conflitar com o System.Windows.Input
using ICommand = Store.Domain.Commands.Interfaces.ICommand;


namespace Store.Domain.Commands;

public class CreateOrderCommand : Notifiable, ICommand
{

    public Guid Product { get; set; }
    public int Quantity { get; set; }
    
    public CreateOrderCommand()
    {
        
    }
    
    
    
    public void Validate()
    {
        
    }
}
