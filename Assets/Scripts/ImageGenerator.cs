using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ImageGenerator : MonoBehaviour
{
    // public Renderer cubeRenderer; // Assign the cube's renderer here via Inspector
    public List<Renderer> cubeRenderers;
    private float interval = 1.2f;
    private string apiUrl = "https://api.openai.com/v1/images/generations"; // Corrected URL
    private string apiKey = "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";
    private Dictionary<int, string> prompts = new Dictionary<int, string>()
        {
            { 0, "Create a surrealist poster combining both image and Dadaist fake news text. The image must depict fear and paranoia: melting humanoid shadows, clocks with sharp teeth, twisted buildings, and crimson fog rolling across a decaying cityscape. Lighting should be high-contrast and dramatic, casting warped shadows. Overlay a short, absurd, and disturbing fake news headline in the style of Dada poetry—nonlinear, nonsensical, unsettling. Suggested example: “Midnight screams elected mayor as clocks demand sacrifice.”Use torn or jittery typography that blends with the image. Avoid photorealism, symmetry, smiling faces, or calm scenery." }, //afraid
            { 1, "Design a dreamlike surrealist image combined with a calm, absurd fake news caption.The scene should feature floating teacups, feathered trees, levitating rivers, and soft glowing skies. Choose pastel tones like lavender and sky blue, with ambient lighting. The fake news text should be gentle but surreal—poetic nonsense suggesting a utopian absurdity. Example: “Clouds unite in silent meditation; parliament replaced by hummingbirds.”Text should flow smoothly within the scene in curved or handwritten fonts.Avoid harsh contrasts, conflict, violence, or digital textures." }, //apaiser
            { 2, "Create a chaotic surrealist poster blending a stressed, overwhelming image with an absurdist Dada news line.Include glitching spiral clocks, broken typewriters with wires, screaming newspaper faces, and tangled networks. Use neon reds, greys, and flickering lighting. The fake news headline should evoke cognitive overload and anxiety. Suggested style: “All fish report burnout; calendars begin therapy.”Text should be fragmented, jagged, or glitchy, interwoven with the scene.Avoid natural harmony, minimalism, or soothing design elements." },//enerve
            { 3, "Generate a frozen surrealist image with cold emotional undertones and an accompanying icy Dadaist headline.Include crystalline forests, statues with glowing blue eyes, frozen skies, and clocks suspended in frost. The lighting should be sterile and cold.Create a Dada-style fake news caption that is absurd and emotionally frigid. Example: “Penguins indicted for excessive stillness; hot chocolate banned by glaciers.”Text should appear as if etched in ice or cracked glass, integrated into the frost-covered scene.Avoid warmth, sunshine, fire, or any organic or tropical motifs." },//glacer
            { 4, "Compose a surrealist visual narrative of stillness and balance paired with poetic Dadaist news.Show floating orbs with closed eyes, pastel landscapes, fog blankets, and gently drifting fabric. Use pale pinks, greens, and ivory, with soft lighting.Add a peaceful yet absurd fake news caption, like: “Tuesdays to be inhaled slowly; silence issued in biodegradable envelopes.”Typography should be centered or gently flowing, matching the serene environment.Avoid busy, sharp, or aggressive visuals and language." },//calm
            { 5, "Design a surrealist poster expressing alienation and technological disconnection, accompanied by a glitchy Dadaist headline.Visuals should include static-filled skies, headless mannequins, glitched human forms, and tangled wires sprouting from concrete. Colors: grey static, error red, neon blue. Lighting: sterile and digital.The fake news caption must suggest absurd digital collapse. Example: “Vowels retired; birds now dream in binary.”Typography should appear pixelated, fractured, or scattered across the image.Avoid nature, harmony, warm colors, or coherent design." }
        };
        
        //version2 
        // {
        //     { 0, "Créez une affiche surréaliste combinant une image d’angoisse paranoïaque avec un titre de fausse nouvelle absurde, dans un style dadaïste. L’image doit montrer des ombres humaines fondantes, des horloges aux dents pointues, des bâtiments tordus et un brouillard cramoisi envahissant une ville en ruines. Utilisez une lumière dramatique à fort contraste, projetant des ombres déformées. ⚠️ Le texte intégré à l’image doit être en **français**, lisible, et visuellement intégré dans le décor — par exemple, avec une typographie déchirée ou tremblante. Exemple : « Les cris de minuit élus maire, les horloges réclament un sacrifice ». Évitez le photoréalisme, la symétrie, les visages souriants ou les paysages apaisants." },

        //     { 1, "Imaginez une image surréaliste onirique accompagnée d’un titre de fausse nouvelle absurde, poétique et calme. La scène doit inclure des tasses de thé flottantes, des arbres à plumes, des rivières en lévitation et un ciel doux aux tons pastel (lavande, bleu ciel). La lumière doit être ambiante et enveloppante. ⚠️ Le **texte doit être en français**, lisible et intégré dans la scène à l’aide d’une typographie manuscrite ou incurvée. Exemple : « Les nuages méditent ensemble ; les colibris remplacent le parlement ». Évitez les contrastes durs, la violence ou les textures numériques." },

        //     { 2, "Créez une affiche surréaliste chaotique mêlant une image oppressante à un titre de fausse nouvelle dadaïste absurde. Intégrez des horloges spiralées en bug, des machines à écrire cassées avec des câbles, des visages de journaux qui hurlent, et des réseaux emmêlés. Couleurs : rouge néon, gris, lumière clignotante. ⚠️ Le **texte (en français)** doit être lisible, intégré dans la scène via une typographie fragmentée ou glitchée. Exemple : « Tous les poissons en burn-out ; les calendriers entament une thérapie ». Évitez les éléments naturels, le minimalisme ou la lisibilité faible." },

        //     { 3, "Générez une image surréaliste glacée avec des tonalités émotionnelles froides et un titre de fausse nouvelle absurde dans un style dadaïste. Incluez des forêts cristallines, des statues aux yeux bleus lumineux, un ciel gelé et des horloges suspendues dans le givre. Lumière stérile et glacée. ⚠️ Le **texte (en français)** doit être clairement visible, comme gravé dans la glace ou le verre fissuré. Exemple : « Les pingouins accusés d'immobilité excessive ; le chocolat chaud interdit par les glaciers ». Évitez toute chaleur, éléments tropicaux ou symboles de vie organique." },

        //     { 4, "Composez une image surréaliste exprimant l'immobilité et l'équilibre, accompagnée d’un titre de fausse nouvelle poétique et absurde. Montrez des orbes flottants aux yeux clos, des paysages pastels, du brouillard léger, des tissus flottants. Couleurs douces : rose pâle, vert tendre, ivoire. ⚠️ Le **texte en français** doit être lisible, intégré au centre ou flotter doucement, avec une typographie fluide. Exemple : « Les mardis seront désormais respirés lentement ; le silence livré en enveloppes biodégradables ». Évitez les éléments agressifs, bruyants ou anguleux." },

        //     { 5, "Concevez une affiche surréaliste exprimant l’aliénation numérique avec un titre de fausse nouvelle absurde et glitchée. Visuels : ciel rempli de parasites, mannequins sans tête, corps humains pixelisés, câbles sortant du béton. Couleurs : gris statique, rouge erreur, bleu néon. Lumière froide et artificielle. ⚠️ Le **texte (en français)** doit être bien visible, stylisé en typographie pixelisée ou fragmentée. Exemple : « Les voyelles prennent leur retraite ; les oiseaux rêvent en binaire ». Évitez les éléments naturels, les couleurs chaudes ou l’harmonie visuelle." }
        // };

    private void Start()
    {
        StartCoroutine(GenerateImageRoutine());
    }

    private IEnumerator GenerateImageRoutine()
    {
        while (true)
        {
            yield return GenerateAndApplyImage();
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator GenerateAndApplyImage()
    {
        int index = Random.Range(0, prompts.Count); //make the '5' maximu, as possible in terms of odd
        Debug.Log("******Random Index (ImageGeneration)= "+index);

        //assign the prompts type to deicide the load in avatar later
        SceneTransferData.promptsType = index;
        Debug.Log("****** Index promptsType (ImageGeneration)= "+SceneTransferData.promptsType);

        string selectedPrompt = prompts[index];

        // string prompt = "Generate a franceinfo fake news with a profile and 30 words shown under the profile that represent this news, make sure the generated image"; //Here is the promt we can customize
        string prompt = selectedPrompt;
        Debug.Log("******Prompt (ImageGeneration)= "+prompt);

        // JSON payload
        string jsonBody = JsonUtility.ToJson(new ImagePrompt { prompt = prompt });

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Image generation failed: " + request.error);
            yield break;
        }

        // Assume response contains direct image URL
        string imageUrl = ParseImageUrl(request.downloadHandler.text);
        yield return StartCoroutine(DownloadImage(imageUrl));
    }

    private IEnumerator DownloadImage(string imageUrl)
    {
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return textureRequest.SendWebRequest();

        if (textureRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            for (int i = 0; i < cubeRenderers.Count; i++)
            {
                
                Material[] materies = cubeRenderers[i].materials;

                for (int j = 0; j < materies.Length; j++)
                {
                    //clear materials
                    materies[j].SetTexture("_MainTex", null);  // Albedo texture
                    materies[j].SetTexture("_EmissionMap", null);

                    materies[j].SetTexture("_MainTex", texture);
                    materies[j].SetTexture("_EmissionMap", texture);
                }
                    
                // cubeRenderers[i].material.mainTexture = texture;
            }
            // cubeRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Image download failed: " + textureRequest.error);
        }
    }

    private string ParseImageUrl(string jsonResponse)
    {
        var result = JsonUtility.FromJson<OpenAIImageResponse>(jsonResponse);
        return result.data[0].url;
    }

    [System.Serializable]
    public class OpenAIImageResponse
    {
        public ImageData[] data;
    }

    [System.Serializable]
    public class ImageData
    {
        public string url;
    }


    [System.Serializable]
    public class ImagePrompt
    {
        // public string model = "dall-e-3";
        public string prompt;
        public int n = 1;
        // public string size = "563x1218";//standtard for our demo
        public string size = "256x256";//in case of test, save money
    }
}





