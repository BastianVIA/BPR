using Frontend.Entities;
using Frontend.Networking;
using Frontend.Service;

namespace Frontend.Model;

public class TesterErrorsModel : ITesterErrorsModel
{
    private INetwork _network;

    public TesterErrorsModel(INetwork network)
    {
        _network = network;
    }

    public async Task<List<TesterErrorsSet>> GetTestErrorsForTesters(List<string> testers, TesterTimePeriodEnum timePeriod)
    {
        var response = await _network.GetTestErrorForTesters(testers, timePeriod);
        //var response = MockData();
        return response.ErrorsForTesters.Select(tester => new TesterErrorsSet
        {
            Name = tester.Name, Errors = FromResponse(tester.Errors)
        }).ToList();
    }

    private List<TesterErrorEntry> FromResponse(IEnumerable<GetTestErrorForTestersError> errors)
    {
        return errors.Select(error => new TesterErrorEntry{ Date = error.Date, ErrorCount = error.ErrorCount }).ToList();
    }

    private List<GetTestErrorForTestersTester> MockData()
    {
        return new List<GetTestErrorForTestersTester>
        {
            new ()
            {
                Name = "CelleNavn1",
                Errors = new List<GetTestErrorForTestersError>
                {
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 01), ErrorCount = 86 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 02), ErrorCount = 45 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 03), ErrorCount = 13 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 04), ErrorCount = 117 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 05), ErrorCount = 4 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 06), ErrorCount = 78 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 07), ErrorCount = 22 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 08), ErrorCount = 19 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 09), ErrorCount = 15 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 10), ErrorCount = 46 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 11), ErrorCount = 45 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 12), ErrorCount = 22 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 13), ErrorCount = 86 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 14), ErrorCount = 45 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 15), ErrorCount = 13 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 16), ErrorCount = 117 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 17), ErrorCount = 4 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 18), ErrorCount = 78 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 19), ErrorCount = 22 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 20), ErrorCount = 19 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 21), ErrorCount = 15 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 22), ErrorCount = 46 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 23), ErrorCount = 45 },
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 24), ErrorCount = 22 }
                }
            },
            new ()
            {
                Name = "CelleNavn2",
                Errors = new List<GetTestErrorForTestersError>
                {
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 01), ErrorCount = 77 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 02), ErrorCount = 40 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 03), ErrorCount = 11 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 04), ErrorCount = 105 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 05), ErrorCount = 3 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 06), ErrorCount = 70 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 07), ErrorCount = 19 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 08), ErrorCount = 17 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 09), ErrorCount = 13 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 10), ErrorCount = 41 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 11), ErrorCount = 40 }, // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 12), ErrorCount = 19 },  // Reduced by 10%
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 13), ErrorCount = 77 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 14), ErrorCount = 40 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 15), ErrorCount = 11 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 16), ErrorCount = 105 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 17), ErrorCount = 3 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 18), ErrorCount = 70 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 19), ErrorCount = 19 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 20), ErrorCount = 17 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 21), ErrorCount = 13 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 22), ErrorCount = 41 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 23), ErrorCount = 40 }, // Reduced by 10%
                new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 24), ErrorCount = 19 }  // Reduced by 10%



                }
            },
            new ()
            {
                Name = "CelleNavn3",
                Errors = new List<GetTestErrorForTestersError>
                {
                    new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 01), ErrorCount = 64 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 02), ErrorCount = 33 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 03), ErrorCount = 9 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 04), ErrorCount = 88 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 05), ErrorCount = 2 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 06), ErrorCount = 58 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 07), ErrorCount = 16 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 08), ErrorCount = 14 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 09), ErrorCount = 11 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 10), ErrorCount = 34 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 11), ErrorCount = 33 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 12), ErrorCount = 16 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 13), ErrorCount = 64 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 14), ErrorCount = 33 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 15), ErrorCount = 9 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 16), ErrorCount = 88 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 17), ErrorCount = 2 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 18), ErrorCount = 58 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 19), ErrorCount = 16 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 20), ErrorCount = 14 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 21), ErrorCount = 11 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 22), ErrorCount = 34 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 23), ErrorCount = 33 }, // Reduced by 25%
new GetTestErrorForTestersError { Date = new DateTime(2019, 01, 24), ErrorCount = 16 }  // Reduced by 25%

                }
            
            }
        };
    }
}