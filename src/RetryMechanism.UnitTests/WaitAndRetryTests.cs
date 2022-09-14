namespace RetryMechanism.UnitTests;

public class WaitAndRetryTests
{
    [Fact]
    public void Should_Return_The_Result_When_The_First_Try_Is_Successful()
    {
        Func<int, int> waitTimeProdiver = (_) => _;
        int result = RetryMechanism.WaitAndRetry<int>(() => { return 123; }, 3, waitTimeProdiver, (_) => { });

        result.Should().Be(123);
    }

    [Fact]
    public void Should_Return_The_Result_After_Retrying_One_Time()
    {
        Queue<Func<int>> functionExectionsQueue = new Queue<Func<int>>();
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the initial try"));
        functionExectionsQueue.Enqueue(() => 123);

        int result = RetryMechanism.WaitAndRetry<int>(() => functionExectionsQueue.Dequeue()(), 3, (_) => _, (_) => { });

        result.Should().Be(123);
    }

    [Fact]
    public void Should_Return_The_Result_After_Retrying_Two_Times()
    {
        Queue<Func<int>> functionExectionsQueue = new Queue<Func<int>>();
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the initial try"));
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the first retry"));
        functionExectionsQueue.Enqueue(() => 123);

        int result = RetryMechanism.WaitAndRetry<int>(() => functionExectionsQueue.Dequeue()(), 3, (_) => _, (_) => { });

        result.Should().Be(123);
    }

    [Fact]
    public void Should_Return_The_Result_After_Retrying_Three_Times()
    {
        Queue<Func<int>> functionExectionsQueue = new Queue<Func<int>>();
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the initial try"));
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the first retry"));
        functionExectionsQueue.Enqueue(() => throw new Exception("Some error on the second retry"));
        functionExectionsQueue.Enqueue(() => 123);

        int result = RetryMechanism.WaitAndRetry<int>(() => functionExectionsQueue.Dequeue()(), 3, (_) => _, (_) => { });

        result.Should().Be(123);
    }
}