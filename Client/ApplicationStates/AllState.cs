namespace Client.ApplicationStates
{
    public class AllState
    {


        public Action? Action { get; set; }
        public bool ShowItem { get; set; }
        public void ItemClicked()
        {
            ResetAllStates();
            ShowItem = true;
            Action?.Invoke();
        }

        public bool ShowItemRequest { get; set; }
        public void ItemRequestClicked()
        {
            ResetAllStates();
            ShowItemRequest = true;
            Action?.Invoke();
        }

        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllStates();
            ShowUser = true;
            Action?.Invoke();

        }



        public void ResetAllStates()
        {
            ShowItem = false;
            ShowItemRequest = false;
            ShowUser = false;
        }

    }
}
