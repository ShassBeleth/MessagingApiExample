using MessagingApiTemplate.Models.Requests.SendMessage.Template.Carousel;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template {

	/// <summary>
	/// カルーセルテンプレート
	/// </summary>
	public class CarouselTemplate : TemplateBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "carousel";

		/// <summary>
		/// カラム
		/// </summary>
		public CarouselColumn[] columns;

		/// <summary>
		/// 画像のアスペクト比
		/// </summary>
		public string imageAspectRatio;

		/// <summary>
		/// 画像の表示形式
		/// </summary>
		public string imageSize;

	}

}