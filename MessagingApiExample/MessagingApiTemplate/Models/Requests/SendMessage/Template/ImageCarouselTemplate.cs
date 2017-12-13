using MessagingApiTemplate.Models.Requests.SendMessage.Template.ImageCarousel;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template {

	/// <summary>
	/// 画像カルーセルテンプレート
	/// </summary>
	public class ImageCarouselTemplate : TemplateBase {

		/// <summary>
		/// テンプレート種別
		/// </summary>
		public string type = "image_carousel";

		/// <summary>
		/// カラム
		/// </summary>
		public ImageCarouselColumn[] columns;

	}

}