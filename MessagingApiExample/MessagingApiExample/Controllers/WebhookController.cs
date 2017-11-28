using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MessagingApiExample.Models.Request.Webhook.Body;
using MessagingApiExample.Models.Webhook.Body.Event;
using MessagingApiExample.Models.Webhook.Body.Event.Message;
using MessagingApiExample.Services.JTokenConverter;
using Newtonsoft.Json.Linq;

namespace MessagingApiExample.Controllers {

	/// <summary>
	/// Messaging APIよりコールされるWebhook API
	/// </summary>
	public class WebhookController : ApiController {

		/// <summary>
		/// POSTメソッド
		/// </summary>
		/// <param name="requestToken">リクエストトークン</param>
		/// <returns>常にステータス200のみを返す</returns>
		public async Task<HttpResponseMessage> Post( JToken requestToken ) {

			Trace.TraceInformation( "Webhook API Start" );

			// リクエストトークンをデータモデルに変換
			ConvertJTokenService convertJTokenService = new ConvertJTokenService();
			WebhookRequest webhookRequest = convertJTokenService.ConvertJTokenToWebhookRequest( requestToken );

			// TODO 署名の検証

			// TODO チャンネルアクセストークンの取得
			// AuthenticationService authenticationService = new AuthenticationService();
			// ChannelAccessTokenResponse channelAccessToken = await authenticationService.IssueChannelAccessToken();

			foreach( EventBase webhookEvent in webhookRequest.events ) {

				switch( webhookEvent ) {

					// 友達追加時イベント
					case FollowEvent followEvent:
						break;

					// ブロック時イベント
					case UnfollowEvent unfollowEvent:
						break;

					// グループ追加時イベント
					case JoinEvent joinEvent:
						break;

					// グループ退会時イベント
					case LeaveEvent leaveEvent:
						break;

					// メッセージイベント
					case MessageEvent messageEvent:
						
						switch( messageEvent.message ) {

							// 音声
							case AudioMessage audioMessage:
								break;

							// ファイル
							case FileMessage fileMessage:
								break;

							// 画像
							case ImageMessage imageMessage:
								break;

							// 位置情報
							case LocationMessage locationMessage:
								break;

							// スタンプ
							case StickerMessage stickerMessage:
								break;

							// テキスト
							case TextMessage textMessage:
								break;

							// 動画
							case VideoMessage videoMessage:
								break;

								// 想定外のイベントの時は何もしない
							default:
								Trace.TraceError( "Unexpected Type" );
								break;

						}

						break;

					// ポストバックイベント
					case PostbackEvent postbackEvent:
						break;

					// ビーコンイベント
					case BeaconEvent beaconEvent:
						break;

					// 想定外のイベントの時は何もしない
					default:
						Trace.TraceError( "Unexpected Message Type" );
						break;

				}

			}

			Trace.TraceInformation( "Webhook API End" );
			return new HttpResponseMessage( HttpStatusCode.OK );

		}

		/*
		
		/// <summary>
		/// 追加時イベント
		/// 友達登録、ブロック解除時、グループ追加時、トークルーム追加時
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		private async Task ExecuteJoinEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId
		) {

			Trace.TraceInformation( "Join Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent( channelAccessToken , replyToken , "追加されました" );

			Trace.TraceInformation( "Join Event End" );

		}

		/// <summary>
		/// 退出時イベント
		/// ブロック時、グループ退出時
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">ユーザIDまたはグループID</param>
		private void ExecuteLeaveEvent(
			string channelAccessToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId
		) {

			Trace.TraceInformation( "Leave Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );

			// TODO ここにイベント内容を記載

			Trace.TraceInformation( "Leave Event End" );

		}

		/// <summary>
		/// テキストメッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="text">テキスト</param>
		private async Task ExecuteTextMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string text
		) {

			Trace.TraceInformation( "Text Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Text is : " + text );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			switch( text ) {

				case "プロフィール":
					await this.ReplyProfileMessageSampleEvent( replyToken , channelAccessToken , sourceId );
					break;

				case "カンファーム":
					await this.ReplyConfirmMessageSampleEvent( replyToken , channelAccessToken );
					break;

				case "ボタン":
					await this.ReplyButtonMessageSampleEvent( replyToken , channelAccessToken );
					break;

				case "カルーセル":
					await this.ReplyCarouselMessageSampleEvent( replyToken , channelAccessToken );
					break;

				default:
					await this.ReplyTextMessageSampleEvent( replyToken , channelAccessToken , "テキスト\n" + text );
					break;

			}

			Trace.TraceInformation( "Text Message Event End" );

		}

		/// <summary>
		/// 画像メッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="binaryImage">バイナリ画像</param>
		private async Task ExecuteImageMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			byte[] binaryImage
		) {

			Trace.TraceInformation( "Image Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Binary Image Length is : " + binaryImage.Length );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent( replyToken , channelAccessToken , "画像" );

			Trace.TraceInformation( "Image Message Event End" );

		}

		/// <summary>
		/// 動画メッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="binaryVideo">バイナリ動画</param>
		private async Task ExecuteVideoMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			byte[] binaryVideo
		) {

			Trace.TraceInformation( "Video Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Binary Video Length is : " + binaryVideo.Length );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent( replyToken , channelAccessToken , "動画" );

			Trace.TraceInformation( "Video Message Event End" );

		}

		/// <summary>
		/// 音声メッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="binaryAudio">バイナリ音声</param>
		private async Task ExecuteAudioMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			byte[] binaryAudio
		) {

			Trace.TraceInformation( "Audio Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Binary Audio Length is : " + binaryAudio.Length );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent( replyToken , channelAccessToken , "音声" );

			Trace.TraceInformation( "Audio Message Event End" );

		}

		/// <summary>
		/// ファイルメッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="fileName">ファイル名</param>
		/// <param name="fileSize">ファイルのバイト数</param>
		/// <param name="binaryFile">バイナリファイル</param>
		private async Task ExecuteFileMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string fileName ,
			string fileSize ,
			byte[] binaryFile
		) {

			Trace.TraceInformation( "File Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "File Name Id is : " + fileName );
			Trace.TraceInformation( "File Size Id is : " + fileSize );
			Trace.TraceInformation( "Binary File Length is : " + binaryFile.Length );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent(
				replyToken ,
				channelAccessToken ,
				"ファイル\n" +
				"ファイル名:" + fileName + "\n" +
				"ファイルサイズ:" + fileSize
			);

			Trace.TraceInformation( "File Message Event End" );

		}

		/// <summary>
		/// 位置情報メッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="title">タイトル</param>
		/// <param name="address">住所</param>
		/// <param name="latitude">緯度</param>
		/// <param name="longitude">経度</param>
		private async Task ExecuteLocationMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string title ,
			string address ,
			string latitude ,
			string longitude
		) {

			Trace.TraceInformation( "Location Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Title is : " + title );
			Trace.TraceInformation( "Address is : " + address );
			Trace.TraceInformation( "Latitude is : " + latitude );
			Trace.TraceInformation( "Longitude is : " + longitude );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent(
				replyToken ,
				channelAccessToken ,
				"位置情報\n" +
				"タイトル:" + title +
				"住所:" + address +
				"緯度:" + latitude +
				"経度:" + longitude
			);

			Trace.TraceInformation( "Location Message Event End" );

		}

		/// <summary>
		/// Stickerメッセージイベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">イベント送信元ID</param>
		/// <param name="packageId">パッケージ識別子</param>
		/// <param name="stickerId">Sticker識別子</param>
		private async Task ExecuteStickerMessageEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string packageId ,
			string stickerId
		) {

			Trace.TraceInformation( "Sticker Message Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Package Id is : " + packageId );
			Trace.TraceInformation( "Sticker Id is : " + stickerId );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent(
				replyToken ,
				channelAccessToken ,
				"Sticker\n" +
				"パッケージ:" + packageId +
				"Sticker:" + stickerId
			);

			Trace.TraceInformation( "Sticker Message Event End" );

		}

		/// <summary>
		/// ポストバック送信時イベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceI">イベント送信元種別</param>
		/// <param name="sourceId">ユーザIDまたはグループIDまたはトークルームID</param>
		/// <param name="data">ポストバックデータ</param>
		private async Task ExecutePostbackEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string data
		) {

			Trace.TraceInformation( "Postback Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Data is : " + data );

			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent( replyToken , channelAccessToken , "ポストバック\n" + data );

			Trace.TraceInformation( "Postback Event End" );

		}

		/// <summary>
		/// ビーコンデバイスの受信圏内出入り時イベント
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="timestamp">Webhook受信日時</param>
		/// <param name="sourceType">イベント送信元種別</param>
		/// <param name="sourceId">ユーザIDまたはグループIDまたはトークルームID</param>
		/// <param name="hardWareId">ハードウェア識別子</param>
		/// <param name="beaconType">ビーコン種別</param>
		/// <param name="deviceMessage">デバイスメッセージ</param>
		private async Task ExecuteBeaconEvent(
			string channelAccessToken ,
			string replyToken ,
			string timestamp ,
			WebhookRequest.Event.Source.SourceType sourceType ,
			string sourceId ,
			string hardWareId ,
			WebhookRequest.Event.Beacon.BeaconType beaconType ,
			string deviceMessage
		) {

			Trace.TraceInformation( "Beacon Event Start" );
			Trace.TraceInformation( "Channel Access Token is : " + channelAccessToken );
			Trace.TraceInformation( "Reply Token is : " + replyToken );
			Trace.TraceInformation( "Timestamp is : " + timestamp );
			Trace.TraceInformation( "Source Type is : " + sourceType );
			Trace.TraceInformation( "Source Id is : " + sourceId );
			Trace.TraceInformation( "Hard Ware Id is : " + hardWareId );
			Trace.TraceInformation( "Beacon Type is : " + beaconType );
			Trace.TraceInformation( "Device Message is : " + deviceMessage );


			// TODO ここにイベント内容を記載
			// 以下サンプル
			await this.ReplyTextMessageSampleEvent(
				replyToken ,
				channelAccessToken ,
				"ビーコン\n" +
				"ハードウェア:" + hardWareId + "\n" +
				"種別:" + beaconType + "\n" +
				"デバイスメッセージ" + deviceMessage
			);

			Trace.TraceInformation( "Beacon Event End" );

		}

		/// <summary>
		/// テキストサンプル
		/// </summary>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="text">テキスト</param>
		/// <returns></returns>
		private async Task ReplyTextMessageSampleEvent(
			string replyToken ,
			string channelAccessToken ,
			string text
		) => await new ReplyMessageService( replyToken , channelAccessToken )
				.AddTextMessage( text )
				.Send();

		/// <summary>
		/// プロフィールを表示するサンプル
		/// </summary>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		private async Task ReplyProfileMessageSampleEvent(
			string replyToken ,
			string channelAccessToken ,
			string userId
		) {

			ResponseOfProfile profile = await new ProfileService().GetProfile( userId , channelAccessToken );

			await new ReplyMessageService( replyToken , channelAccessToken )
				.AddTextMessage(
					"プロフィール\n" +
					"表示名：" + profile.displayName + "\n" +
					"ステータスメッセージ：" + profile.statusMessage
				)
				.Send();

		}

		/// <summary>
		/// Confirmを表示するサンプル
		/// </summary>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <returns></returns>
		private async Task ReplyConfirmMessageSampleEvent(
			string replyToken ,
			string channelAccessToken
		) => await new ReplyMessageService( replyToken , channelAccessToken )
				.AddConfirmMessage(
					"代替テキスト" ,
					"メッセージ" ,
					new ReplyMessageService.ActionCreator()
						.CreateAction( Models.ReplyMessage.RequestOfReplyMessage.Message.Template.TemplateType.Confirm )
						.AddMessageAction( "メッセージ" , "ただメッセージ送るだけ" )
						.AddPostbackAction( "ポストバック" , "データ" , "ポストバック送信" )
						.GetActions()
				)
				.Send();

		/// <summary>
		/// Buttonを表示するサンプル
		/// </summary>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <returns></returns>
		private async Task ReplyButtonMessageSampleEvent(
			string replyToken ,
			string channelAccessToken
		) => await new ReplyMessageService( replyToken , channelAccessToken )
				.AddButtonsMessage(
					"代替テキスト" ,
					"https://www.j-cast.com/assets_c/2016/09/news_20160926195911-thumb-autox380-94951.png" ,
					"タイトル" ,
					"メッセージ" ,
					new ReplyMessageService.ActionCreator()
						.CreateAction( Models.ReplyMessage.RequestOfReplyMessage.Message.Template.TemplateType.Buttons )
						.AddMessageAction( "メッセージ１" , "メッセージ！" )
						.AddMessageAction( "メッセージ２" , "メッセージ！！" )
						.AddMessageAction( "メッセージ３" , "メッセージ！！！" )
						.GetActions()
				)
				.Send();

		/// <summary>
		/// カルーセルを表示するサンプル
		/// </summary>
		/// <param name="replyToken">リプライトークン</param>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <returns></returns>
		private async Task ReplyCarouselMessageSampleEvent(
			string replyToken ,
			string channelAccessToken
		) => await new ReplyMessageService( replyToken , channelAccessToken )
				.AddCarouselMessage(
					"代替テキスト" ,
					new ReplyMessageService.ColumnCreator()
						.CreateColumn()
						.AddColumn(
							"https://cdn-ak.f.st-hatena.com/images/fotolife/k/konayuki358/20160903/20160903081936.png" ,
							"タイトル１" ,
							"テキスト１" ,
							new ReplyMessageService.ActionCreator()
								.CreateAction( Models.ReplyMessage.RequestOfReplyMessage.Message.Template.TemplateType.Carousel )
								.AddMessageAction( "テキスト" , "テキスト！" )
								.AddPostbackAction( "ポストバック" , "データ" , "テキスト！" )
								.GetActions()
						)
						.AddColumn(
							"https://static.curazy.com/wp-content/uploads/2016/09/29111206_hidoi_reply.png" ,
							"タイトル２" ,
							"テキスト２" ,
							new ReplyMessageService.ActionCreator()
								.CreateAction( Models.ReplyMessage.RequestOfReplyMessage.Message.Template.TemplateType.Carousel )
								.AddMessageAction( "テキスト２" , "テキスト！！" )
								.AddPostbackAction( "ポストバック２" , "データ２" , "テキスト！！！" )
								.GetActions()
						)
						.GetColumns()
				)
				.Send();

		}

	*/
	}

}