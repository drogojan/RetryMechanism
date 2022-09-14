namespace RetryMechanism.UnitTests;

public class WaitAndRetryTests
{
    [Fact]
    public void Should_Return_The_Result_When_The_First_Try_Is_Successful()
    {
        Func<int, int> waitTimeProdiver = (_) => _;
        int result = RetryMechanism.WaitAndRetry<int>(() => { return 123; }, 3, waitTimeProdiver, (_) => {});

        result.Should().Be(123);
    }
}