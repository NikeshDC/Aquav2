public struct Counter
{
    private int maxCount;
    private int count;

    public Counter(int maxCount)
    {
        this.maxCount = maxCount;
        this.count = maxCount;
    }

    public bool TryDecrement()
    {
        if( count == 0)
            return false;
        count--;
        return true;
    }

    public void Reset()
    {
        this.count = this.maxCount;
    }
}
