using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template {

	/// <summary>
	/// ボタンテンプレート
	/// </summary>
	public class ButtonTemplate : TemplateBase {

		/// <summary>
		/// テンプレート種別
		/// </summary>
		public string type = "buttons";

		/// <summary>
		/// サムネ画像URL
		/// </summary>
		public string thumbnailImageUrl;

		/// <summary>
		/// 画像のアスペクト比
		/// </summary>
		public string imageAspectRatio;

		/// <summary>
		/// 画像の表示形式
		/// </summary>
		public string imageSize;

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