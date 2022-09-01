namespace Grid
{
    public class RandomLettersPool : GameObjectPool<ITile>
    {
        protected override void OnElementGet(ITile obj)
        {
            obj.SetActive(true);
        }

        protected override void OnElementReturn(ITile obj)
        {
            obj.SetActive(false);
        }
    }
}