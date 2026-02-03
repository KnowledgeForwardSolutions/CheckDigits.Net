namespace CheckDigits.Net.DataAnnotations.Tests.Unit.TestData;

public static class TestDataExtensions
{
   public static ValidationAttribute ToCheckDigitAttribute(this String algorithmName)
      => algorithmName switch
      {
         nameof(AlphanumericMod97_10Algorithm) => new CheckDigitAttribute<AlphanumericMod97_10Algorithm>(),
         nameof(AbaRtnAlgorithm) => new CheckDigitAttribute<AbaRtnAlgorithm>(),
         nameof(CusipAlgorithm) => new CheckDigitAttribute<CusipAlgorithm>(),
         nameof(DammAlgorithm) => new CheckDigitAttribute<DammAlgorithm>(),
         nameof(FigiAlgorithm) => new CheckDigitAttribute<FigiAlgorithm>(),
         nameof(IbanAlgorithm) => new CheckDigitAttribute<IbanAlgorithm>(),
         nameof(Icao9303Algorithm) => new CheckDigitAttribute<Icao9303Algorithm>(),
         nameof(Icao9303MachineReadableVisaAlgorithm) => new CheckDigitAttribute<Icao9303MachineReadableVisaAlgorithm>(),
         nameof(Icao9303SizeTD1Algorithm) => new CheckDigitAttribute<Icao9303SizeTD1Algorithm>(),
         nameof(Icao9303SizeTD2Algorithm) => new CheckDigitAttribute<Icao9303SizeTD2Algorithm>(),
         nameof(Icao9303SizeTD3Algorithm) => new CheckDigitAttribute<Icao9303SizeTD3Algorithm>(),
         nameof(IsanAlgorithm) => new CheckDigitAttribute<IsanAlgorithm>(),
         nameof(IsinAlgorithm) => new CheckDigitAttribute<IsinAlgorithm>(),
         nameof(Iso6346Algorithm) => new CheckDigitAttribute<Iso6346Algorithm>(),
         nameof(Iso7064CustomDanishAlgorithm) => new CheckDigitAttribute<Iso7064CustomDanishAlgorithm>(),
         nameof(Iso7064CustomLettersAlgorithm) => new CheckDigitAttribute<Iso7064CustomLettersAlgorithm>(),
         nameof(Iso7064CustomNumericSupplementalAlgorithm) => new CheckDigitAttribute<Iso7064CustomNumericSupplementalAlgorithm>(),
         nameof(Iso7064Mod11_10Algorithm) => new CheckDigitAttribute<Iso7064Mod11_10Algorithm>(),
         nameof(Iso7064Mod11_2Algorithm) => new CheckDigitAttribute<Iso7064Mod11_2Algorithm>(),
         nameof(Iso7064Mod1271_36Algorithm) => new CheckDigitAttribute<Iso7064Mod1271_36Algorithm>(),
         nameof(Iso7064Mod27_26Algorithm) => new CheckDigitAttribute<Iso7064Mod27_26Algorithm>(),
         nameof(Iso7064Mod37_2Algorithm) => new CheckDigitAttribute<Iso7064Mod37_2Algorithm>(),
         nameof(Iso7064Mod37_36Algorithm) => new CheckDigitAttribute<Iso7064Mod37_36Algorithm>(),
         nameof(Iso7064Mod661_26Algorithm) => new CheckDigitAttribute<Iso7064Mod661_26Algorithm>(),
         nameof(Iso7064Mod97_10Algorithm) => new CheckDigitAttribute<Iso7064Mod97_10Algorithm>(),
         nameof(LuhnAlgorithm) => new CheckDigitAttribute<LuhnAlgorithm>(),
         nameof(Modulus10_13Algorithm) => new CheckDigitAttribute<Modulus10_13Algorithm>(),
         nameof(Modulus10_1Algorithm) => new CheckDigitAttribute<Modulus10_1Algorithm>(),
         nameof(Modulus10_2Algorithm) => new CheckDigitAttribute<Modulus10_2Algorithm>(),
         nameof(Modulus11Algorithm) => new CheckDigitAttribute<Modulus11Algorithm>(),
         nameof(NcdAlgorithm) => new CheckDigitAttribute<NcdAlgorithm>(),
         nameof(NhsAlgorithm) => new CheckDigitAttribute<NhsAlgorithm>(),
         nameof(NpiAlgorithm) => new CheckDigitAttribute<NpiAlgorithm>(),
         nameof(SedolAlgorithm) => new CheckDigitAttribute<SedolAlgorithm>(),
         nameof(VerhoeffAlgorithm) => new CheckDigitAttribute<VerhoeffAlgorithm>(),
         nameof(VinAlgorithm) => new CheckDigitAttribute<VinAlgorithm>(),
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static IFooRequest ToFooRequest(this String algorithmName)
      => algorithmName switch
      {
         nameof(AlphanumericMod97_10Algorithm) => new FooAlphanumericMod97_10(),
         nameof(AbaRtnAlgorithm) => new FooAbaRtn(),
         nameof(CusipAlgorithm) => new FooCusip(),
         nameof(DammAlgorithm) => new FooDamm(),
         nameof(FigiAlgorithm) => new FooFigi(),
         nameof(IbanAlgorithm) => new FooIban(),
         nameof(Icao9303Algorithm) => new FooIcao9303(),
         nameof(Icao9303MachineReadableVisaAlgorithm) => new FooIcao9303MachineReadableVisa(),
         nameof(Icao9303SizeTD1Algorithm) => new FooIcao9303SizeTD1(),
         nameof(Icao9303SizeTD2Algorithm) => new FooIcao9303SizeTD2(),
         nameof(Icao9303SizeTD3Algorithm) => new FooIcao9303SizeTD3(),
         nameof(IsanAlgorithm) => new FooIsan(),
         nameof(IsinAlgorithm) => new FooIsin(),
         nameof(Iso6346Algorithm) => new FooIso6346(),
         nameof(Iso7064CustomDanishAlgorithm) => new FooIso7064CustomDanishAlphabet(),
         nameof(Iso7064CustomLettersAlgorithm) => new FooIso7064CustomLettersAlphabet(),
         nameof(Iso7064CustomNumericSupplementalAlgorithm) => new FooIso7064CustomNumericSupplementalAlphabet(),
         nameof(Iso7064Mod11_10Algorithm) => new FooIso7064Mod11_10(),
         nameof(Iso7064Mod11_2Algorithm) => new FooIso7064Mod11_2(),
         nameof(Iso7064Mod1271_36Algorithm) => new FooIso7064Mod1271_36(),
         nameof(Iso7064Mod27_26Algorithm) => new FooIso7064Mod27_26(),
         nameof(Iso7064Mod37_2Algorithm) => new FooIso7064Mod37_2(),
         nameof(Iso7064Mod37_36Algorithm) => new FooIso7064Mod37_36(),
         nameof(Iso7064Mod661_26Algorithm) => new FooIso7064Mod661_26(),
         nameof(Iso7064Mod97_10Algorithm) => new FooIso7064Mod97_10(),
         nameof(LuhnAlgorithm) => new FooLuhn(),
         nameof(Modulus10_13Algorithm) => new FooModulus10_13(),
         nameof(Modulus10_1Algorithm) => new FooModulus10_1(),
         nameof(Modulus10_2Algorithm) => new FooModulus10_2(),
         nameof(Modulus11Algorithm) => new FooModulus11(),
         nameof(NcdAlgorithm) => new FooNcd(),
         nameof(NhsAlgorithm) => new FooNhs(),
         nameof(NpiAlgorithm) => new FooNpi(),
         nameof(SedolAlgorithm) => new FooSedol(),
         nameof(VerhoeffAlgorithm) => new FooVerhoeff(),
         nameof(VinAlgorithm) => new FooVin(),
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static String ToInvalidMaskedRequestValue(this String algorithmName)
      => algorithmName switch
      {
         nameof(LuhnAlgorithm) => "3059 6300 0902 0004",                               // Diners Club test card number with two digit transposition error 69 -> 96 
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static String ToInvalidRequestValue(this String algorithmName)
      => algorithmName switch
      {
         nameof(AbaRtnAlgorithm) => "122238821",                                       // US Bank with single digit transcription error 5 -> 8
         nameof(AlphanumericMod97_10Algorithm) => "SC74MC0LB1031234567890123456USD",   // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
         nameof(CusipAlgorithm) => "037844100",                                        // 037833100 with two digit twin error 33 -> 44
         nameof(DammAlgorithm) => "112233445566778899016",                             // Single digit errors (using "112233445566778899006" as a valid value)
         nameof(FigiAlgorithm) => "BBG000M9P426",                                      // BBG000N9P426 with single character transcription error N -> M
         nameof(IbanAlgorithm) => "AL32502111090000000001234567",                      // AL35202111090000000001234567 with two digit transposition error 52 -> 25
         nameof(Icao9303Algorithm) => "7438122",                                       // 7408122 with single digit transcription error (0 -> 3)
         nameof(Icao9303MachineReadableVisaAlgorithm) => "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<E231458907UTO7408122F1204159<<<<<<<<<<<<<<<<",               // Format A, D231458907 with single character transcription error (D -> E)
         //nameof(Icao9303MachineReadableVisaAlgorithm) => "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408212F1204159<<<<<<<<",                               // Format B, 7408122 with two digit transposition error (12 -> 21)
         nameof(Icao9303SizeTD1Algorithm) => "I<UTOE231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<<<",                         // D231458907 with single character transcription error (D -> E)
         nameof(Icao9303SizeTD2Algorithm) => "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<E231458907UTO7408122F1204159<<<<<<<6",                                           // D231458907 with single character transcription error (D -> E)
         nameof(Icao9303SizeTD3Algorithm) => "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<17",                           // ZE184226B<<<<<1 with two char transposition error (ZE -> EZ)
         nameof(IsanAlgorithm) => "0000ABCD1234FEDC72112AABB6",                        // 0000ABCD1234FEDC71122AABB6 with jump transposition error 112 -> 211
         nameof(IsinAlgorithm) => "US02079J1079",                                      // US02079K1079 with single character transcription error K -> J
         nameof(Iso6346Algorithm) => "MEDU7807688",                                    // MEDU8707688 with two digit transposition error 87 -> 78 
         nameof(Iso7064CustomDanishAlgorithm) => "SØSTESDA",                           // Danish word for sister "SØSTER" with single char transcription error R -> S
         nameof(Iso7064CustomLettersAlgorithm) => "QWERTUDVORAKY",                     // QWERTYDVORAKY with single char transcription error Y -> U
         nameof(Iso7064CustomNumericSupplementalAlgorithm) => "0000000444767411",
         nameof(Iso7064Mod11_10Algorithm) => "114433446",                              // // 112233446 with two digit twin error 22 -> 44
         nameof(Iso7064Mod11_2Algorithm) => "0000000736691440",                        // 0000000073669144 with circular shift error
         nameof(Iso7064Mod1271_36Algorithm) => "XT868977863229AU",                     // XS868977863229AU with single char transcription error S -> T
         nameof(Iso7064Mod27_26Algorithm) => "QWERTUDVORAKY",                          // QWERTYDVORAKY with single char transcription error Y -> U
         nameof(Iso7064Mod37_2Algorithm) => "A999920212346*",                          // A999922012346* with two digit transposition error 20 -> 02
         nameof(Iso7064Mod37_36Algorithm) => "QWERDYTVORAK1",                          // QWERTYDVORAK1 with jump transposition error TYD -> DYT
         nameof(Iso7064Mod661_26Algorithm) => "RASDFQWERTYLKJHL",                      // ASDFQWERTYLKJHLR with circular shift error
         nameof(Iso7064Mod97_10Algorithm) => "163217541835191038",                     // 163217581835191038 with single char transcription error 8 -> 4
         nameof(LuhnAlgorithm) => "3059630009020004",                                  // Diners Club test card number with two digit transposition error 69 -> 96 
         nameof(Modulus10_13Algorithm) => "4006383133931",                             // EAN-13 with two digit transposition error (13 -> 31)
         nameof(Modulus10_1Algorithm) => "7742185",                                    // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
         nameof(Modulus10_2Algorithm) => "1020569",                                    // IMO Number 1010569 with single digit transcription error 1 -> 2
         nameof(Modulus11Algorithm) => "050029273X",                                   // ISBN-10 with jump transposition 729 -> 927
         nameof(NcdAlgorithm) => "13030/xd93gt2q",                                     // 13030/xf93gt2q with single char transcription error f -> d
         nameof(NhsAlgorithm) => "3946787881",                                         // Valid NHS number (9876544321) with jump transposition 674 -> 467
         nameof(NpiAlgorithm) => "1238560971",                                         // Valid NPI 1234560971 with single digit transcription error 4 -> 8
         nameof(SedolAlgorithm) => "3174865",                                          // 3134865 with single digit transcription error 3 -> 7
         nameof(VerhoeffAlgorithm) => "84736430459837284567892",                       // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
         nameof(VinAlgorithm) => "1G8ZG217XWZ157259",                                  // Two character transposition 12 -> 21
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static ValidationAttribute ToMaskedCheckDigitAttribute(this String algorithmName)
      => algorithmName switch
      {
         nameof(LuhnAlgorithm) => new MaskedCheckDigitAttribute<LuhnAlgorithm, CreditCardMask>(),
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static IFooRequest ToMaskedFooRequest(this String algorithmName)
      => algorithmName switch
      {
         nameof(LuhnAlgorithm) => new FooMaskedLuhn(),
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static String ToValidMaskedRequestValue(this String algorithmName)
      => algorithmName switch
      {
         nameof(LuhnAlgorithm) => "4012 8888 8888 1881",                               // Visa test credit card number
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };

   public static String ToValidRequestValue(this String algorithmName)
      => algorithmName switch
      {
         nameof(AbaRtnAlgorithm) => "111000025",                                       // Worked example from Wikipedia (https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit)
         nameof(AlphanumericMod97_10Algorithm) => "10Bx939c5543TqA1144M999143X38",     // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
         nameof(CusipAlgorithm) => "037833100",                                        // Apple
         nameof(DammAlgorithm) => "123456789018",                                      // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
         nameof(FigiAlgorithm) => "BBG000BLNQ16",                                      // Example from https://www.openfigi.com/assets/content/figi-check-digit-2173341b2d.pdf
         nameof(IbanAlgorithm) => "GB82WEST12345698765432",                            // Worked example from Wikipedia https://en.wikipedia.org/wiki/International_Bank_Account_Number
         nameof(Icao9303Algorithm) => "7408122",                                       // Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
         nameof(Icao9303MachineReadableVisaAlgorithm) => "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C<3UTO6908061F9406236ZE184226B<<<<<<<",                 // Format A, Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
         //nameof(Icao9303MachineReadableVisaAlgorithm) => "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<L898902C<3UTO6908061F9406236ZE184226",                                 // Format B, Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
         nameof(Icao9303SizeTD1Algorithm) => "I<UTOD231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<<<",                           // Example MRZ from https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf
         nameof(Icao9303SizeTD2Algorithm) => "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408122F1204159<<<<<<<6",                                             // Example MRZ from https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf
         nameof(Icao9303SizeTD3Algorithm) => "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10",                             // Example MRZ from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
         nameof(IsanAlgorithm) => "00000000D0A90011C000000001",                        // Full ISAN for Star Trek Next Gen episode "Yesterday's Enterprise"
         nameof(IsinAlgorithm) => "GB0031348658",                                      // Barclays
         nameof(Iso6346Algorithm) => "CSQU3054383",                                    // Worked example from Wikipedia
         nameof(Iso7064CustomDanishAlgorithm) => "SØSTERDA",                           // Danish word for sister "SØSTER"
         nameof(Iso7064CustomLettersAlgorithm) => "QWERTYDVORAKY",
         nameof(Iso7064CustomNumericSupplementalAlgorithm) => "07940",
         nameof(Iso7064Mod11_10Algorithm) => "07945",                                  // Example from ISO 7064 specification
         nameof(Iso7064Mod11_2Algorithm) => "0000000073669144",                        // ISNI for Richard, Zachary from https://isni.org/page/search-database/  
         nameof(Iso7064Mod1271_36Algorithm) => "ISO793W",                              // Example from ISO/IEC 7064 specification
         nameof(Iso7064Mod27_26Algorithm) => "QWERTYDVORAKY",                          // From CheckDigits.Net.Tests.Unit.Iso7064Mod27_26AlgorithmTests
         nameof(Iso7064Mod37_2Algorithm) => "A999914123456N",                          // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_9c7ba55fbdd44a80947bc310cdd92382.pdf
         nameof(Iso7064Mod37_36Algorithm) => "00000000C36D002B00000000E",              // Full ISAN for Star Trek episode "Amok Time"
         nameof(Iso7064Mod661_26Algorithm) => "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZNS",      // From CheckDigits.Net.Tests.Unit.Iso7064Mod661_26AlgorithmTests
         nameof(Iso7064Mod97_10Algorithm) => "1011339391255432926101144229991433338",  // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
         nameof(LuhnAlgorithm) => "4012888888881881",                                  // Visa test credit card number
         nameof(Modulus10_13Algorithm) => "036000291452",                              // Worked UPC-A example from Wikipedia (https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation)
         nameof(Modulus10_1Algorithm) => "7732185",                                    // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
         nameof(Modulus10_2Algorithm) => "9074729",                                    // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
         nameof(Modulus11Algorithm) => "1568656521",                                   // ISBN-10 Island in the Stream of Time, S. M. Sterling
         nameof(NcdAlgorithm) => "13030/xf93gt2q",                                     // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
         nameof(NhsAlgorithm) => "9434765919",                                         // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
         nameof(NpiAlgorithm) => "1245319599",                                         // Example from www.hippaspace.com
         nameof(SedolAlgorithm) => "BKPBC67",                                          // Google bond
         nameof(VerhoeffAlgorithm) => "84736430954837284567892",                       // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
         nameof(VinAlgorithm) => "1M8GDM9AXKP042788",                                  // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
         _ => throw new ArgumentOutOfRangeException(nameof(algorithmName), algorithmName, "Unrecognized algorithm name")
      };
}
