using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Models.Config.Webhook {
	
	/// <summary>
	/// WebhookServiceの設定項目
	/// </summary>
	public class WebhookServiceConfig {
		
		/// <summary>
		/// リクエスト
		/// </summary>
		public JToken RequestJToken { set; get; }

		/// <summary>
		/// リクエストヘッダ
		/// </summary>
		public HttpRequestHeaders RequestHeaders { set; get; }

		/// <summary>
		/// リクエストコンテント
		/// </summary>
		public HttpContent RequestContent { set; get; }

		/// <summary>
		/// 署名の検証をするかどうか
		/// </summary>
		public bool IsExecuteVerifySign { set; get; } = true;

		/// <summary>
		/// ロングタームチャンネルアクセストークンを使用するかどうか
		/// </summary>
		public bool IsUseLongTermChannelAccessToken { set; get; } = false;

		/// <summary>
		/// フォローイベント
		/// </summary>
		public Func< string , string , Task > FollowEventHandler { set; get; } = null ;

		/// <summary>
		/// 参加イベント
		/// </summary>
		public Func<string , string , Task> JoinEventHandler { set; get; } = null;

		/// <summary>
		/// 退出イベント
		/// </summary>
		public Action LeaveEventHandler { set; get; } = null ;

		/// <summary>
		/// 音声メッセージイベント
		/// </summary>
		public Action AudioMessageEventHandler { set; get; } = null;

		/// <summary>
		/// ファイルメッセージイベント
		/// </summary>
		public Action FileMessageEventHandler { set; get; } = null ;

		/// <summary>
		/// 画像メッセージイベント
		/// </summary>
		public Action ImageMessageEventHandler { set; get; } = null;

		/// <summary>
		/// 位置情報メッセージイベント
		/// </summary>
		public Action LocationMessageEventHandler { set; get; } = null ;

		/// <summary>
		/// スタンプメッセージイベント
		/// </summary>
		public Action StickerMessageEventHandler { set; get; } = null;

		/// <summary>
		/// テキストメッセージイベント
		/// </summary>
		public Action TextMessageEventHandler { set; get; } = null ;

		/// <summary>
		/// 動画メッセージイベント
		/// </summary>
		public Action VideoMessageEventHandler { set; get; } = null;

		/// <summary>
		/// ポストバックイベント
		/// </summary>
		public Action PostbackMessageEventHandler { set; get; } = null ;

		/// <summary>
		/// フォロー解除イベント
		/// </summary>
		public Action UnfollowEventHandler { set; get; } = null;

		/// <summary>
		/// ビーコンバナータップイベント
		/// </summary>
		public Action BannerBeaconEventHandler { set; get; } = null ;

		/// <summary>
		/// ビーコン受信圏内入りイベント
		/// </summary>
		public Action EnterBeaconEventHandler { set; get; } = null;

		/// <summary>
		/// ビーコン受信圏外出イベント
		/// </summary>
		public Action LeaveBeaconEventHandler { set; get; } = null;

	}

}