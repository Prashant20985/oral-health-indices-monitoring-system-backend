using App.Persistence.Attributes;

namespace App.Application.Test.Behavior;

public class SampleData
{
    // Sample request and response for testing
    public class SampleRequest { }
    public class SampleResponse { }

    [OralEhrContextUnitOfWork]
    public class SampleRequestWithUnitOfWorkAttribute { }
}
