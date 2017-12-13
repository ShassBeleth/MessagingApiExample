using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.Carousel {

	/// <summary>
	/// カルーセルのカラム
	/// </summary>
	public class CarouselColumn {

		/// <summary>
		/// 画像URL
		/// </summary>
		public string thumbnailImageUrl;

		/// <summary>
		/// 画像の背景色
		/// </summary>
		public string imageBackgroundColor = "#FFFFFF";

		/// <summary>
		/// タイトル
		/// </summary>
		public string title;

		/// <summary>
		/// メッセージテキスト
		/// </summary>
		public string text;

		/// <summary>
		/// タップ時のアクション
		/// </summary>
		public TemplateActionBase[] actions;

	}

}