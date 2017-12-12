﻿using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using MessagingApiTemplate.Services;

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

			await WebhookService.ExecuteService( 
				requestToken , 
				this.Request.Headers , 
				this.Request.Content ,
				
				// 署名の検証はしない
				false , 
				
				// ロングタームのチャンネルアクセストークンを使用する
				true ,

				// 友達追加、ブロック解除時イベント
				async ( channelAccessToken , replyToken ) => {
					await this.ExecuteFollowEvent( channelAccessToken , replyToken );
				} ,

				// グループ参加時イベント
				( channelAccessToken , replyToken ) => {
					await this.ExecuteJoinEvent( channelAccessToken , replyToken );
				} ,

				// グループ退出時イベント
				() => {
					Trace.TraceInformation( "Execute Leave Event" );
				} ,

				// 音声メッセージイベント
				() => {
					Trace.TraceInformation( "Execute Audio Message Event" );
				} ,
		
				// ファイルメッセージイベント
				() => {
					Trace.TraceInformation( "Execute File Message Event" );
				} ,

				// 画像メッセージイベント
				() => {
					Trace.TraceInformation( "Execute Image Message Event" );
				} ,

				// 位置情報メッセージイベント
				() => {
					Trace.TraceInformation( "Execute Location Message Event" );
				} ,
				
				// スタンプメッセージイベント
				() => {
					Trace.TraceInformation( "Execute Sticker Message Event" );
				} ,

				// テキストメッセージイベント
				() => {
					Trace.TraceInformation( "Execute Text Message Event" );
				} ,

				// 動画メッセージイベント
				() => {
					Trace.TraceInformation( "Execute Video Message Event" );
				} ,

				// ポストバックイベント
				() => {
					Trace.TraceInformation( "Execute Postback Message Event" );
				} ,

				// ブロック時イベント
				() => {
					Trace.TraceInformation( "Execute Unfollow Message Event" );
				} ,

				// バナータップ時イベント
				() => {
					Trace.TraceInformation( "Execute Banner Beacon Message Event" );
				} ,

				// ビーコン受信圏内に入った時のイベント
				() => {
					Trace.TraceInformation( "Execute Enter Beacon Message Event" );
				} ,

				// ビーコン受信圏外に出た時のイベント
				() => {
					Trace.TraceInformation( "Execute Leave Beacon Message Event" );
				}
				
			);
			
			return new HttpResponseMessage( HttpStatusCode.OK );

		}

		/// <summary>
		/// 友達追加、ブロック解除イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteFollowEvent(
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Execute Follow Event" );
			await ReplyMessageService.SendReplyMessage(
				channelAccessToken ,
				replyToken ,
				MessageFactoryService
					.CreateMessage()
					.AddTextMessage( "茜ちゃんやで！友達登録ありがとうな！" )
			);
		}

		/// <summary>
		/// グループ追加イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteJoinEvent(
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Execute Join Event" );
			await ReplyMessageService.SendReplyMessage(
				channelAccessToken ,
				replyToken ,
				MessageFactoryService
					.CreateMessage()
					.AddTextMessage( "茜ちゃんやで！グループ追加ありがとうな！" )
			);
		}

	}

}