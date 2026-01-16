namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public static class Utility
{
   public static IList<ValidationResult> ValidateModel(Object model)
   {
      var results = new List<ValidationResult>();
      var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
      Validator.TryValidateObject(model, validationContext, results, validateAllProperties: true);

      return results;
   }
}
