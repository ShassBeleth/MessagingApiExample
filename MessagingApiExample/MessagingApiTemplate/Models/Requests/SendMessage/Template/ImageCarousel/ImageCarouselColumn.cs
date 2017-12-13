using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.ImageCarousel {

	/// <summary>
	/// 画像カルーセルのカラム
	/// </summary>
	public class ImageCarouselColumn {

		/// <summary>
		/// 画像URL
		/// </summary>
		public string imageUrl;

		/// <summary>
		/// 画像タップ時のアクション
		/// </summary>
		public TemplateActionBase action;

	}

}