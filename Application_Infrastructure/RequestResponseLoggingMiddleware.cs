using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using Application_Core;

namespace Application_Infrastructure
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IResponse_Request response_request)
        {
            //First, get the incoming request
            string request = await FormatRequest(context.Request);
            string Scheme = context.Request.Scheme;
            string Host = context.Request.Host + context.Request.Path;
            string QueryString = context.Request.QueryString.ToString();

            //TODO: Save log to chosen datastore
            int requestid = await response_request.RequestSaveAsync(Scheme, Host, QueryString, null, request);

            //Copy a pointer to the original response body stream
            Stream originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (MemoryStream responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                string response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore
                await response_request.ResponseSaveAsync(null, response, requestid);
                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            //We convert the byte[] into a string using UTF8 encoding...
            string bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            //return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
            return $"{bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            //return $"{response.StatusCode}‡ {text}";
            return $"{text}";
        }
    }
}