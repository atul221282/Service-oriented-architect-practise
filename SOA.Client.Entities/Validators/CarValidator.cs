using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities.Validators
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.RentalPrice).GreaterThan(0);
            RuleFor(x => x.Year).GreaterThan(2000).LessThan(DateTime.Now.Year);
        }
    }
}
