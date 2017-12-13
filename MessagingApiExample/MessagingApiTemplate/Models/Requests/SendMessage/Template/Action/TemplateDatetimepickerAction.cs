namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.Action {

	/// <summary>
	/// 日付選択アクション
	/// </summary>
	public class TemplateDatetimepickerAction : TemplateActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "datetimepicker";

		/// <summary>
		/// ラベル
		/// </summary>
		public string label;

		/// <summary>
		/// ポストバックデータ
		/// </summary>
		public string data;

		/// <summary>
		/// アクションモード
		/// </summary>
		public string mode;

		/// <summary>
		/// 初期値
		/// </summary>
		public string initial;

		/// <summary>
		/// 選択可能な日付の最大値
		/// </summary>
		public string max;

		/// <summary>
		/// 選択可能な日付の最小値
		/// </summary>
		public string min;

	}

}