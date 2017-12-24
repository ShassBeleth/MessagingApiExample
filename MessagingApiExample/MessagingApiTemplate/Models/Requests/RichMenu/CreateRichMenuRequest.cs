using MessagingApiTemplate.Models.Responses.RichMenu;

namespace MessagingApiTemplate.Models.Requests.RichMenu {

	public class CreateRichMenuRequest {

		public SizeObject size;

		public bool selected;

		public string name;

		public string chatBarText;

		public Area[] areas;

	}

}