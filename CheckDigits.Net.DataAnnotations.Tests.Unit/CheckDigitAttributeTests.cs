// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Check digit validation failed";

   public class CustomMessageRequest
   {
      [CheckDigit<LuhnAlgorithm>(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class RequiredFieldRequest
   {
      [Required, CheckDigit<Modulus10_13Algorithm>]
      public String Upc { get; set; } = null!;
   }

   public class IntegerTypeRequest
   {
      [CheckDigit<LuhnAlgorithm>]
      public Int32 ItemNumber { get; set; }
   }

   public static TheoryData<IFooRequest> ValidRequests => new()
   {
      { new FooAbaRtn { BarValue = "111000025" } },                                       // Worked example from Wikipedia (https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit)
      { new FooAlphanumericMod97_10 { BarValue = "10Bx939c5543TqA1144M999143X38" } },     // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
      { new FooCusip { BarValue = "037833100" } },                                        // Apple
      { new FooDamm { BarValue = "123456789018" } },                                      // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
      { new FooFigi { BarValue = "BBG000BLNQ16" } },                                      // Example from https://www.openfigi.com/assets/content/figi-check-digit-2173341b2d.pdf
      { new FooIban { BarValue = "GB82WEST12345698765432" } },                            // Worked example from Wikipedia https://en.wikipedia.org/wiki/International_Bank_Account_Number
      { new FooIcao9303 { BarValue = "7408122" } },                                       // Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
      { new FooIcao9303MachineReadableVisa { BarValue = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C<3UTO6908061F9406236ZE184226B<<<<<<<" } },                 // Format A, Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
      { new FooIcao9303MachineReadableVisa { BarValue = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<L898902C<3UTO6908061F9406236ZE184226" } },                                 // Format B, Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
      { new FooIcao9303SizeTD1 { BarValue = "I<UTOD231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<<<" } },                           // Example MRZ from https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf
      { new FooIcao9303SizeTD2 { BarValue = "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408122F1204159<<<<<<<6" } },                                             // Example MRZ from https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf
      { new FooIcao9303SizeTD3 { BarValue = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10" } },                             // Example MRZ from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
      { new FooIsan { BarValue = "00000000D0A90011C000000001" } },                        // Full ISAN for Star Trek Next Gen episode "Yesterday's Enterprise"
      { new FooIsin { BarValue = "GB0031348658" } },                                      // Barclays
      { new FooIso6346 { BarValue = "CSQU3054383" } },                                    // Worked example from Wikipedia
      { new FooIso7064Mod11_10 { BarValue = "07945" } },                                  // Example from ISO 7064 specification
      { new FooIso7064Mod11_2 { BarValue = "0000000073669144" } },                        // ISNI for Richard, Zachary from https://isni.org/page/search-database/  
      { new FooIso7064Mod1271_36 { BarValue = "ISO793W" } },                              // Example from ISO/IEC 7064 specification
      { new FooIso7064Mod27_26 { BarValue = "QWERTYDVORAKY" } },                          // From CheckDigits.Net.Tests.Unit.Iso7064Mod27_26AlgorithmTests
      { new FooIso7064Mod37_2 { BarValue = "A999914123456N" } },                          // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_9c7ba55fbdd44a80947bc310cdd92382.pdf
      { new FooIso7064Mod37_36 { BarValue = "00000000C36D002B00000000E" } },              // Full ISAN for Star Trek episode "Amok Time"
      { new FooIso7064Mod661_26 { BarValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZNS" } },      // From CheckDigits.Net.Tests.Unit.Iso7064Mod661_26AlgorithmTests
      { new FooIso7064Mod97_10 { BarValue = "1011339391255432926101144229991433338" } },  // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
      { new FooLuhn { BarValue = "4012888888881881" } },                                  // Visa test credit card number
      { new FooModulus10_13 { BarValue = "036000291452" } },                              // Worked UPC-A example from Wikipedia (https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation)
      { new FooModulus10_1 { BarValue = "7732185" } },                                    // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
      { new FooModulus10_2 { BarValue = "9074729" } },                                    // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
      { new FooModulus11 { BarValue = "1568656521" } },                                   // ISBN-10 Island in the Stream of Time, S. M. Sterling
      { new FooNcd { BarValue = "13030/xf93gt2q" } },                                     // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
      { new FooNhs { BarValue = "9434765919" } },                                         // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
      { new FooNpi { BarValue = "1245319599" } },                                         // Example from www.hippaspace.com
      { new FooSedol { BarValue = "BKPBC67" } },                                          // Google bond
      { new FooVerhoeff { BarValue = "84736430954837284567892" } },                       // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
      { new FooVin { BarValue = "1M8GDM9AXKP042788" } },                                  // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
   };

   public static TheoryData<IFooRequest> InvalidRequests => new()
   {
      { new FooAbaRtn { BarValue = "122238821" } },                                       // US Bank with single digit transcription error 5 -> 8
      { new FooAlphanumericMod97_10 { BarValue = "SC74MC0LB1031234567890123456USD" } },   // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
      { new FooCusip { BarValue = "037844100" } },                                        // 037833100 with two digit twin error 33 -> 44
      { new FooDamm { BarValue = "112233445566778899016" } },                             // Single digit errors (using "112233445566778899006" as a valid value)
      { new FooFigi { BarValue = "BBG000M9P426" } },                                      // BBG000N9P426 with single character transcription error N -> M
      { new FooIban { BarValue = "AL32502111090000000001234567" } },                      // AL35202111090000000001234567 with two digit transposition error 52 -> 25
      { new FooIcao9303 { BarValue = "7438122" } },                                       // 7408122 with single digit transcription error (0 -> 3)
      { new FooIcao9303MachineReadableVisa { BarValue = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<E231458907UTO7408122F1204159<<<<<<<<<<<<<<<<" } },               // Format A, D231458907 with single character transcription error (D -> E)
      { new FooIcao9303MachineReadableVisa { BarValue = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408212F1204159<<<<<<<<" } },                               // Format B, 7408122 with two digit transposition error (12 -> 21)
      { new FooIcao9303SizeTD1 { BarValue = "I<UTOE231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<<<" } },                         // D231458907 with single character transcription error (D -> E)
      { new FooIcao9303SizeTD2 { BarValue = "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<E231458907UTO7408122F1204159<<<<<<<6" } },                                           // D231458907 with single character transcription error (D -> E)
      { new FooIcao9303SizeTD3 { BarValue = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<17" } },                           // ZE184226B<<<<<1 with two char transposition error (ZE -> EZ)
      { new FooIsan { BarValue = "0000ABCD1234FEDC72112AABB6" } },                        // 0000ABCD1234FEDC71122AABB6 with jump transposition error 112 -> 211
      { new FooIsin { BarValue = "US02079J1079" } },                                      // US02079K1079 with single character transcription error K -> J
      { new FooIso6346 { BarValue = "MEDU7807688" } },                                    // MEDU8707688 with two digit transposition error 87 -> 78 
      { new FooIso7064Mod11_2 { BarValue = "0000000736691440" } },                        // 0000000073669144 with circular shift error
      { new FooIso7064Mod1271_36 { BarValue = "XT868977863229AU" } },                     // XS868977863229AU with single char transcription error S -> T
      { new FooIso7064Mod27_26 { BarValue = "QWERTUDVORAKY" } },                          // QWERTYDVORAKY with single char transcription error Y -> U
      { new FooIso7064Mod37_2 { BarValue = "A999920212346*" } },                          // A999922012346* with two digit transposition error 20 -> 02
      { new FooIso7064Mod37_36 { BarValue = "QWERDYTVORAK1" } },                          // QWERTYDVORAK1 with jump transposition error TYD -> DYT
      { new FooIso7064Mod661_26 { BarValue = "RASDFQWERTYLKJHL" } },                      // ASDFQWERTYLKJHLR with circular shift error
      { new FooIso7064Mod97_10 { BarValue = "163217541835191038" } },                     // 163217581835191038 with single char transcription error 8 -> 4
      { new FooLuhn { BarValue = "3059630009020004" } },                                  // Diners Club test card number with two digit transposition error 69 -> 96 
      { new FooModulus10_13 { BarValue = "4006383133931" } },                             // EAN-13 with two digit transposition error (13 -> 31)
      { new FooModulus10_1 { BarValue = "7742185" } },                                    // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
      { new FooModulus10_2 { BarValue = "1020569" } },                                    // IMO Number 1010569 with single digit transcription error 1 -> 2
      { new FooModulus11 { BarValue = "050029273X" } },                                   // ISBN-10 with jump transposition 729 -> 927
      { new FooNcd { BarValue = "13030/xd93gt2q" } },                                     // 13030/xf93gt2q with single char transcription error f -> d
      { new FooNhs { BarValue = "3946787881" } },                                         // Valid NHS number (9876544321) with jump transposition 674 -> 467
      { new FooNpi { BarValue = "1238560971" } },                                         // Valid NPI 1234560971 with single digit transcription error 4 -> 8
      { new FooSedol { BarValue = "3174865" } },                                          // 3134865 with single digit transcription error 3 -> 7
      { new FooVerhoeff { BarValue = "84736430459837284567892" } },                       // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
      { new FooVin { BarValue = "1G8ZG217XWZ157259" } },                                  // Two character transposition 12 -> 21
   };

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(ValidRequests))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidCheckDigit(IFooRequest request)
   {
      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(ValidRequests))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull(IFooRequest request)
   {
      // Arrange.
      request.BarValue = null!;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(ValidRequests))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty(IFooRequest request)
   {
      // Arrange.
      request.BarValue = String.Empty;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new IntegerTypeRequest
      {
         ItemNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ItemNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new RequiredFieldRequest();
      var expectedMessage = "The Upc field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("")]
   [InlineData("  ")]
   [InlineData("\t")]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmptyOrWhitespace(String upc)
   {
      // Arrange.
      var request = new RequiredFieldRequest
      {
         Upc = upc
      };
      var expectedMessage = "The Upc field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [MemberData(nameof(InvalidRequests))]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidCheckDigit(IFooRequest request)
   {
      var expectedMessage = "The field BarValue is invalid.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidCheckDigitAndCustomErrorMessageIsSupplied()
   {
      // Arrange.
      var request = new CustomMessageRequest
      {
         CardNumber = "5558555555554444"           // MasterCard test card number with single digit transcription error 5 -> 8
      };
      var expectedMessage = _customErrorMessage;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   #endregion
}
