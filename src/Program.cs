using FluentValidation.Results;
using MateDogfood;
using Newtonsoft.Json;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var customer = new Customer { Name = "Ada Lovelace", Email = "ada@example.com", Age = 36 };

var validator = new CustomerValidator();
ValidationResult result = validator.Validate(customer);

Log.Information("Customer: {Json}", JsonConvert.SerializeObject(customer));
Log.Information("Valid: {IsValid}", result.IsValid);

foreach (var error in result.Errors)
{
    Log.Warning("{Property}: {Message}", error.PropertyName, error.ErrorMessage);
}
