namespace TT
{
    public class SpecialPaper : InventoryItemObject
    {
        private Player Player;
        protected override void Awake()
        {
            base.Awake();
            Player = FindObjectOfType<Player>();
            OnPickUpEvent.AddListener(() =>
            {
                Player.isAcquiredSpecialPaper = true;
            });
        }
    }
}
