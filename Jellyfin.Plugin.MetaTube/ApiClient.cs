using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using Jellyfin.Plugin.MetaTube.Metadata;
#if __EMBY__
using MediaBrowser.Common.Net;
#endif

namespace Jellyfin.Plugin.MetaTube;

public static class ApiClient
{
    private const string ActorInfoApi = "/v1/actors";
    private const string MovieInfoApi = "/v1/movies";
    private const string ActorSearchApi = "/v1/actors/search";
    private const string MovieSearchApi = "/v1/movies/search";
    private const string PrimaryImageApi = "/v1/images/primary";
    private const string ThumbImageApi = "/v1/images/thumb";
    private const string BackdropImageApi = "/v1/images/backdrop";
    private const string TranslateApi = "/v1/translate";

    /// <summary>
    /// 构建API请求的完整URL。
    /// </summary>
    /// <param name="path">API路径，如"/v1/movies"。</param>
    /// <param name="nv">包含查询参数的键值对集合。</param>
    /// <returns>完整的API请求URL，包括服务器地址、路径和查询参数。</returns>
    private static string ComposeUrl(string path, NameValueCollection nv)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (string key in nv)
            query.Add(key, nv.Get(key));

        // Build URL
        var uriBuilder = new UriBuilder(Plugin.Instance.Configuration.Server)
        {
            Path = path,
            Query = query.ToString() ?? string.Empty,
        };
        return uriBuilder.ToString();
    }

    /// <summary>
    /// 构建MetaTube图片API的完整URL，用于获取或处理媒体图片。
    /// </summary>
    /// <remarks>
    /// 该方法是图片相关API的核心URL构造方法，用于生成获取主要图片、缩略图和背景图片的请求URL。
    /// 它会自动添加插件配置中的默认图片质量参数，并支持多种图片处理选项。
    /// </remarks>
    /// <param name="path">图片API的基础路径，如"/v1/images/primary"、"/v1/images/thumb"或"/v1/images/backdrop"。</param>
    /// <param name="provider">内容提供商名称，如"javlibrary"、"xcity"等。</param>
    /// <param name="id">内容的唯一标识符，通常是视频或演员的ID。</param>
    /// <param name="url">可选的原始图片URL，用于从外部源获取图片。</param>
    /// <param name="ratio">图片宽高比，-1表示使用默认比例。</param>
    /// <param name="position">图片裁剪位置，范围0-1，-1表示使用默认位置。</param>
    /// <param name="auto">是否自动处理图片（如自动裁剪、优化）。</param>
    /// <param name="badge">可选的徽章图片URL，用于在主图片上叠加徽章。</param>
    /// <returns>完整的图片API请求URL字符串。</returns>
    /// <example>
    /// 用法示例：
    /// <code>
    /// // 获取演员的主要图片URL
    /// var url = ComposeImageApiUrl("/v1/images/primary", "javlibrary", "abc123");
    ///
    /// // 获取带有裁剪参数的视频缩略图URL
    /// var url = ComposeImageApiUrl("/v1/images/thumb", "xcity", "xyz789", ratio: 1.78, position: 0.3);
    /// </code>
    /// </example>
    private static string ComposeImageApiUrl(
        string path,
        string provider,
        string id,
        string url = default,
        double ratio = -1,
        double position = -1,
        bool auto = false,
        string badge = default
    )
    {
        return ComposeUrl(
            Path.Combine(path, provider, id),
            new NameValueCollection
            {
                { "url", url },
                { "ratio", ratio.ToString("R") },
                { "pos", position.ToString("R") },
                { "auto", auto.ToString() },
                { "badge", badge },
                { "quality", Plugin.Instance.Configuration.DefaultImageQuality.ToString() },
            }
        );
    }

    private static string ComposeInfoApiUrl(string path, string provider, string id, bool lazy)
    {
        return ComposeUrl(
            Path.Combine(path, provider, id),
            new NameValueCollection { { "lazy", lazy.ToString() } }
        );
    }

    private static string ComposeSearchApiUrl(string path, string q, string provider, bool fallback)
    {
        return ComposeUrl(
            path,
            new NameValueCollection
            {
                { "q", q },
                { "provider", provider },
                { "fallback", fallback.ToString() },
            }
        );
    }

    private static string ComposeTranslateApiUrl(
        string path,
        string q,
        string from,
        string to,
        string engine,
        NameValueCollection nv = null
    )
    {
        return ComposeUrl(
            path,
            new NameValueCollection
            {
                { "q", q },
                { "from", from },
                { "to", to },
                { "engine", engine },
                nv ?? new NameValueCollection(),
            }
        );
    }

    public static string GetPrimaryImageApiUrl(
        string provider,
        string id,
        double position = -1,
        string badge = default
    )
    {
        return ComposeImageApiUrl(
            PrimaryImageApi,
            provider,
            id,
            ratio: Plugin.Instance.Configuration.PrimaryImageRatio,
            position: position,
            badge: badge
        );
    }

    public static string GetPrimaryImageApiUrl(
        string provider,
        string id,
        string url,
        double position = -1,
        bool auto = false,
        string badge = default
    )
    {
        return ComposeImageApiUrl(
            PrimaryImageApi,
            provider,
            id,
            url,
            Plugin.Instance.Configuration.PrimaryImageRatio,
            position,
            auto,
            badge
        );
    }

    public static string GetThumbImageApiUrl(string provider, string id)
    {
        return ComposeImageApiUrl(ThumbImageApi, provider, id);
    }

    public static string GetThumbImageApiUrl(
        string provider,
        string id,
        string url,
        double position = -1,
        bool auto = false
    )
    {
        return ComposeImageApiUrl(ThumbImageApi, provider, id, url, position: position, auto: auto);
    }

    public static string GetBackdropImageApiUrl(string provider, string id)
    {
        return ComposeImageApiUrl(BackdropImageApi, provider, id);
    }

    public static string GetBackdropImageApiUrl(
        string provider,
        string id,
        string url,
        double position = -1,
        bool auto = false
    )
    {
        return ComposeImageApiUrl(
            BackdropImageApi,
            provider,
            id,
            url,
            position: position,
            auto: auto
        );
    }

#if __EMBY__
    public static async Task<HttpResponseInfo> GetImageResponse(
        string url,
        CancellationToken cancellationToken
    )
#else
    public static async Task<HttpResponseMessage> GetImageResponse(
        string url,
        CancellationToken cancellationToken
    )
#endif
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("User-Agent", DefaultUserAgent);
        var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
#if __EMBY__
        return new HttpResponseInfo
        {
            Content = await response
                .Content.ReadAsStreamAsync(cancellationToken)
                .ConfigureAwait(false),
            ContentLength = response.Content.Headers.ContentLength,
            ContentType = response.Content.Headers.ContentType?.ToString(),
            StatusCode = response.StatusCode,
            Headers = response.Content.Headers.ToDictionary(
                kvp => kvp.Key,
                kvp => string.Join(", ", kvp.Value)
            ),
        };
#else
        return response;
#endif
    }

    public static async Task<ActorInfo> GetActorInfoAsync(
        string provider,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await GetActorInfoAsync(
            provider,
            id,
            true /* default */
            ,
            cancellationToken
        );
    }

    public static async Task<ActorInfo> GetActorInfoAsync(
        string provider,
        string id,
        bool lazy,
        CancellationToken cancellationToken
    )
    {
        var apiUrl = ComposeInfoApiUrl(ActorInfoApi, provider, id, lazy);
        return await GetDataAsync<ActorInfo>(apiUrl, true, cancellationToken);
    }

    public static async Task<MovieInfo> GetMovieInfoAsync(
        string provider,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await GetMovieInfoAsync(
            provider,
            id,
            true /* default */
            ,
            cancellationToken
        );
    }

    public static async Task<MovieInfo> GetMovieInfoAsync(
        string provider,
        string id,
        bool lazy,
        CancellationToken cancellationToken
    )
    {
        var apiUrl = ComposeInfoApiUrl(MovieInfoApi, provider, id, lazy);
        return await GetDataAsync<MovieInfo>(apiUrl, true, cancellationToken);
    }

    public static async Task<List<ActorSearchResult>> SearchActorAsync(
        string q,
        CancellationToken cancellationToken
    )
    {
        return await SearchActorAsync(q, string.Empty, cancellationToken);
    }

    public static async Task<List<ActorSearchResult>> SearchActorAsync(
        string q,
        string provider,
        CancellationToken cancellationToken
    )
    {
        return await SearchActorAsync(
            q,
            provider,
            true /* default */
            ,
            cancellationToken
        );
    }

    public static async Task<List<ActorSearchResult>> SearchActorAsync(
        string q,
        string provider,
        bool fallback,
        CancellationToken cancellationToken
    )
    {
        var apiUrl = ComposeSearchApiUrl(ActorSearchApi, q, provider, fallback);
        return await GetDataAsync<List<ActorSearchResult>>(apiUrl, true, cancellationToken);
    }

    public static async Task<List<MovieSearchResult>> SearchMovieAsync(
        string q,
        CancellationToken cancellationToken
    )
    {
        return await SearchMovieAsync(q, string.Empty, cancellationToken);
    }

    public static async Task<List<MovieSearchResult>> SearchMovieAsync(
        string q,
        string provider,
        CancellationToken cancellationToken
    )
    {
        return await SearchMovieAsync(
            q,
            provider,
            true /* default */
            ,
            cancellationToken
        );
    }

    public static async Task<List<MovieSearchResult>> SearchMovieAsync(
        string q,
        string provider,
        bool fallback,
        CancellationToken cancellationToken
    )
    {
        var apiUrl = ComposeSearchApiUrl(MovieSearchApi, q, provider, fallback);
        return await GetDataAsync<List<MovieSearchResult>>(apiUrl, true, cancellationToken);
    }

    public static async Task<TranslationInfo> TranslateAsync(
        string q,
        string from,
        string to,
        string engine,
        NameValueCollection nv,
        CancellationToken cancellationToken
    )
    {
        var apiUrl = ComposeTranslateApiUrl(TranslateApi, q, from, to, engine, nv);
        return await GetDataAsync<TranslationInfo>(apiUrl, false, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="requireAuth"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static async Task<T> GetDataAsync<T>(
        string url,
        bool requireAuth,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        // Add General Headers.
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("User-Agent", DefaultUserAgent);

        // Set API Authorization Token.
        if (requireAuth && !string.IsNullOrWhiteSpace(Plugin.Instance.Configuration.Token))
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                Plugin.Instance.Configuration.Token
            );

        var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        // Nullable forgiving reason:
        // Response is unlikely to be null.
        // If it happens to be null, an exception is planed to be thrown either way.
        var apiResponse = (
            await response
                .Content!.ReadFromJsonAsync<ResponseInfo<T>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false)
        )!;

        // EnsureSuccessStatusCode ignoring reason:
        // When the status is unsuccessful, the API response contains error details.
        if (!response.IsSuccessStatusCode && apiResponse.Error != null)
            throw new Exception(
                $"API request error: {apiResponse.Error.Code} ({apiResponse.Error.Message})"
            );

        // Note: data field must not be null if there are no errors.
        if (apiResponse.Data == null)
            throw new Exception("Response data field is null");

        return apiResponse.Data;
    }

    #region Http

    private static readonly HttpClient HttpClient;
    private static string DefaultUserAgent => $"{Plugin.ProviderName}/{Plugin.Instance.Version}";

    static ApiClient()
    {
        HttpClient = new HttpClient(
            new SocketsHttpHandler
            {
                // Connect Timeout.
                ConnectTimeout = TimeSpan.FromSeconds(30),

                // TCP Keep Alive.
                KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always,
                KeepAlivePingDelay = TimeSpan.FromSeconds(30),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),

                // Connection Pooling.
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(90),
            }
        );
    }

    #endregion
}
