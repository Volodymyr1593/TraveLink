namespace TraveLink.Views.Shared.Components.ViewComponents
{
    public class SearchFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new HotelViewModel();
            return View(model);  // Відправляє модель в часткове представлення
        }


    }
}
