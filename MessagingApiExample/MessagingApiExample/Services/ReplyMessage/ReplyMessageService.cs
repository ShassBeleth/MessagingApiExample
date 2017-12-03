using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Request.ReplyMessage;
using MessagingApiExample.Models.Request.ReplyMessage.Message;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.ReplyMessage {

	/// <summary>
	/// リクエスト送信用サービス
	/// </summary>
	public class ReplyMessageService {

		/// <summary>
		/// チャンネルアクセストークン
		/// </summary>
		private string channelAccessToken;

		/// <summary>
		/// リプライトークン
		/// </summary>
		private string replyToken;

		/// <summary>
		/// リクエスト
		/// </summary>
		private ReplyMessageRequest replyMessageRequest = new ReplyMessageRequest();

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateMessageArray() {

			if( this.replyMessageRequest.messages == null ) {
				this.replyMessageRequest.messages = new MessageBase[ 1 ];
				return true;
			}
			else {
				if( this.replyMessageRequest.messages.Length == 5 ) {
					Trace.TraceWarning( "Messages Length is Max" );
					return false;
				}
				else {
					Array.Resize<MessageBase>(
						ref this.replyMessageRequest.messages ,
						this.replyMessageRequest.messages.Length + 1
					);
					return true;
				}

			}
			
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="replyToken">ReplyToken</param>
		public ReplyMessageService( string channelAccessToken , string replyToken ) {
			this.channelAccessToken = channelAccessToken;
			this.replyToken = replyToken;
			this.replyMessageRequest.messages = null;
		}

		/// <summary>
		/// テキストメッセージ追加
		/// </summary>
		/// <param name="text">テキスト</param>
		public ReplyMessageService AddTextMessage( string text ) {

			if( !this.RegulateMessageArray() )
				return this;
			
			TextMessage textMessage = new TextMessage() {
				text = text
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = textMessage;

			return this;

		}

		/// <summary>
		/// スタンプメッセージ追加
		/// </summary>
		/// <param name="packageId">パッケージID</param>
		/// <param name="stickerId">スタンプID</param>
		public ReplyMessageService AddStickerMessage( string packageId , string stickerId ) {

			if( !this.RegulateMessageArray() )
				return this;

			StickerMessage stickerMessage = new StickerMessage() {
				packageId = packageId ,
				stickerId = stickerId
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = stickerMessage;

			return this;

		}

		/// <summary>
		/// 画像メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">画像URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public ReplyMessageService AddImageMessage( string originalContentUrl , string previewImageUrl ) {

			if( !this.RegulateMessageArray() )
				return this;

			ImageMessage imageMessage = new ImageMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = imageMessage;

			return this;

		}

		/// <summary>
		/// 動画メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">動画URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public ReplyMessageService AddVideoMessage( string originalContentUrl , string previewImageUrl ) {

			if( !this.RegulateMessageArray() )
				return this;

			VideoMessage videoMessage = new VideoMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = videoMessage;

			return this;

		}

		/// <summary>
		/// 音声メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">音声URL</param>
		/// <param name="duration">音声ファイルの長さ</param>
		public ReplyMessageService AddAudioMessage( string originalContentUrl , int duration ) {

			if( !this.RegulateMessageArray() )
				return this;

			AudioMessage audioMessage = new AudioMessage() {
				originalContentUrl = originalContentUrl ,
				duration = duration
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = audioMessage;

			return this;

		}

		/// <summary>
		/// 位置情報メッセージ追加
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="address">住所</param>
		/// <param name="latitude">緯度</param>
		/// <param name="longitude">経度</param>
		public ReplyMessageService AddLocationMessage(
			string title ,
			string address ,
			decimal latitude ,
			decimal longitude
		) {

			if( !this.RegulateMessageArray() )
				return this;

			LocationMessage locationMessage = new LocationMessage() {
				title = title ,
				address = address ,
				latitude = latitude ,
				longitude = longitude
			};
			this.replyMessageRequest.messages[ this.replyMessageRequest.messages.Length - 1 ] = locationMessage;
			
			return this;

		}
				
		/// <summary>
		/// メッセージの返信
		/// </summary>
		public async Task SendReplyMessage() {

			this.replyMessageRequest.replyToken = this.replyToken;

			string jsonRequest = JsonConvert.SerializeObject( this.replyMessageRequest );
			Trace.TraceInformation( "Reply Message Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );
			
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + this.channelAccessToken + "}" );

			try {

				HttpResponseMessage response = await client.PostAsync( "https://api.line.me/v2/bot/message/reply" , content );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				content.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Send Reply Message Response is : " + resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Send Reply Message is Argument Null Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Send Reply Message is Http Request Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( Exception ) {
				Trace.TraceError( "Send Reply Message is Unexpected Exception" );
				content.Dispose();
				client.Dispose();
			}

		}
		
	}

}