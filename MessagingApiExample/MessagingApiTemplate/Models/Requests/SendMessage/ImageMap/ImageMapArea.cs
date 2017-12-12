namespace MessagingApiTemplate.Models.Requests.SendMessage.ImageMap {

	/// <summary>
	/// イメージマップ領域オブジェクト
	/// </summary>
	public class ImageMapArea {

		/// <summary>
		/// 領域左上角からの横方向の相対位置
		/// </summary>
		public int x;

		/// <summary>
		/// 領域の左上角からの縦方向の相対位置
		/// </summary>
		public int y;

		/// <summary>
		/// タップ領域の幅
		/// </summary>
		public int width;

		/// <summary>
		/// タップ領域の高さ
		/// </summary>
		public int height;

	}

}