namespace TT
{
    public class SpecialPaper : InventoryItemObject
    {
        protected override void Awake()
        {
            base.Awake();
            
            OnPickUpEvent.AddListener(() =>
            {
                Player.Instance.isAcquiredSpecialPaper = true;
            });
        }
    }
}
