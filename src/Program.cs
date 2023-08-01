using Newtonsoft.Json;

class Program {
    /// <summary>
    /// The global HttpClient to be used.
    /// </summary>
    public static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main() {
        Console.WriteLine("Hello World!");
        Program p = new Program();
        p.Test();
    }

    private void Test() {
        RawData.Music data = JsonConvert.DeserializeObject<RawData.Music>("{\"id\":\"SSsbczG2\",\"title\":\"Mera Pyar Tera Pyar\",\"subtitle\":\"Jeet Gannguli, Arijit Singh - Jalebi (Original Motion Picture Soundtrack)\",\"header_desc\":\"\",\"type\":\"song\",\"perma_url\":\"https://www.jiosaavn.com/song/mera-pyar-tera-pyar/IzsYUxdKcAE\",\"image\":\"http://c.saavncdn.com/619/Jalebi-Hindi-2018-20180921123431-150x150.jpg\",\"language\":\"hindi\",\"year\":\"2018\",\"play_count\":\"37412360\",\"explicit_content\":\"0\",\"list_count\":\"0\",\"list_type\":\"\",\"list\":\"\",\"more_info\":{\"music\":\"Jeet Gannguli\",\"album_id\":\"13923761\",\"album\":\"Jalebi (Original Motion Picture Soundtrack)\",\"label\":\"Sony Music Entertainment India Pvt. Ltd.\",\"origin\":\"playlist\",\"is_dolby_content\":false,\"320kbps\":\"true\",\"encrypted_media_url\":\"iPPGVzyogeiPwpro65A0eUaQggN+8+J48MCC3UppFMldMpqtsXZr3PPsantUNO6aQ1gF6skPjOd7pEzkQxlaqIPzFaL/aK97\",\"encrypted_cache_url\":\"\",\"album_url\":\"https://www.jiosaavn.com/album/jalebi-original-motion-picture-soundtrack/Eh2V,t8LUWY_\",\"duration\":\"275\",\"rights\":{\"code\":\"0\",\"cacheable\":\"true\",\"delete_cached_object\":\"false\",\"reason\":\"\"},\"cache_state\":\"\",\"has_lyrics\":\"false\",\"lyrics_snippet\":\"\",\"starred\":\"false\",\"copyright_text\":\"(P) 2018 Sony Music Entertainment India Pvt. Ltd.\",\"artistMap\":{\"primary_artists\":[{\"id\":\"464912\",\"name\":\"Jeet Gannguli\",\"role\":\"primary_artists\",\"image\":\"https://c.saavncdn.com/artists/Jeet_Gannguli_002_20190506100413_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/jeet-gannguli-songs/37adMqLwAgI_\"},{\"id\":\"459320\",\"name\":\"Arijit Singh\",\"role\":\"primary_artists\",\"image\":\"https://c.saavncdn.com/artists/Arijit_Singh_002_20230323062147_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/arijit-singh-songs/LlRWpHzy3Hk_\"}],\"featured_artists\":[],\"artists\":[{\"id\":\"464912\",\"name\":\"Jeet Gannguli\",\"role\":\"music\",\"image\":\"https://c.saavncdn.com/artists/Jeet_Gannguli_002_20190506100413_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/jeet-gannguli-songs/37adMqLwAgI_\"},{\"id\":\"464912\",\"name\":\"Jeet Gannguli\",\"role\":\"singer\",\"image\":\"https://c.saavncdn.com/artists/Jeet_Gannguli_002_20190506100413_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/jeet-gannguli-songs/37adMqLwAgI_\"},{\"id\":\"459320\",\"name\":\"Arijit Singh\",\"role\":\"singer\",\"image\":\"https://c.saavncdn.com/artists/Arijit_Singh_002_20230323062147_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/arijit-singh-songs/LlRWpHzy3Hk_\"},{\"id\":\"13934123\",\"name\":\"Rashmi Virag\",\"role\":\"lyricist\",\"image\":\"https://c.saavncdn.com/artists/Rashmi_Virag_000_20220920120709_150x150.jpg\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/rashmi-virag-songs/rR69rrf5dlo_\"},{\"id\":\"5370666\",\"name\":\"Varun Mitra\",\"role\":\"starring\",\"image\":\"\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/varun-mitra-songs/YRBFbC4JgtY_\"},{\"id\":\"670896\",\"name\":\"Rhea Chakraborty\",\"role\":\"starring\",\"image\":\"\",\"type\":\"artist\",\"perma_url\":\"https://www.jiosaavn.com/artist/rhea-chakraborty-songs/hcAuIZXS02s_\"}]},\"release_date\":\"2018-09-21\",\"label_url\":\"/label/sony-music-entertainment-india-pvt.-ltd.-albums/LaFAA6h1q2U_\",\"vcode\":\"010910140880306\",\"vlink\":\"https://jiotunepreview.jio.com/content/Converted/010910140835986.mp3\",\"triller_available\":false,\"request_jiotune_flag\":false,\"webp\":\"true\"}}")!;
        ParsedData.Music music = new ParsedData.Music(data);
        music.Download("downloads");
    }
}