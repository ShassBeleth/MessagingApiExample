using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;

namespace MessagingApiTemplate.Models.Responses.RichMenu {

	public class SizeObject {

		/// <summary>
		/// リッチメニューの幅
		/// </summary>
		public int width;

		/// <summary>
		/// リッチメニューの高さ
		/// </summary>
		public int height;

	}

	public class Bounds {

		public int x;
		public int y;
		public int width;
		public int height;

	}
	
	public class Area {

		public Bounds bounds;

		public TemplatePostbackAction action;

	}

	public class GetRichMenuResponse {

		/// <summary>
		/// リッチメニューID
		/// </summary>
		public string richMenuId;

		/// <summary>
		/// サイズオブジェクト
		/// </summary>
		public SizeObject size;

		/// <summary>
		/// デフォルトでリッチメニューを表示するか
		/// </summary>
		public bool selected;

		/// <summary>
		/// リッチメニューの名前
		/// </summary>
		public string name;

		/// <summary>
		/// トークルームメニューに表示されるテキスト
		/// </summary>
		public string chatBarText;

		/// <summary>
		/// タップ領域の座標とサイズ
		/// </summary>
		public Area[] areas;

	}

}