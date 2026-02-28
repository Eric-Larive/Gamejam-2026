using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastorDressController : MonoBehaviour
{
    [SerializeField] private Image castorImage;
    [SerializeField] private CastorSpriteLibrary library; // <-- assign in Inspector
    [SerializeField] private float frameDelay = 0.08f;

    public Outfit CurrentOutfit { get; private set; } = Outfit.None;

    private readonly Dictionary<(CastorMood mood, int frame, Outfit outfit), Sprite> map = new();
    private Coroutine animRoutine;

    void Awake()
    {
        if (castorImage == null) castorImage = GetComponent<Image>();
        BuildMapFromLibrary();
        SetIdle();
    }

    public void EquipAndReact(Outfit piece)
    {
        CurrentOutfit |= piece;
        PlayReaction(CastorMood.Happy);
    }

    public void FailReact()
    {
        PlayReaction(CastorMood.Sad);
    }

    private void SetIdle()
    {
        if (TryGetSprite(CastorMood.Normal, 1, CurrentOutfit, out var s) ||
            TryGetAnyFrame(CastorMood.Normal, CurrentOutfit, out s))
        {
            castorImage.sprite = s;
        }
        else
        {
            Debug.LogWarning($"No NORMAL sprite found for outfit: {CurrentOutfit}");
        }
    }

    private void PlayReaction(CastorMood reactionMood)
    {
        if (animRoutine != null) StopCoroutine(animRoutine);
        animRoutine = StartCoroutine(PlayReactionRoutine(reactionMood));
    }

    private IEnumerator PlayReactionRoutine(CastorMood reactionMood)
    {
        List<Sprite> frames = new();
        for (int f = 1; f <= 20; f++)
        {
            if (TryGetSprite(reactionMood, f, CurrentOutfit, out var s))
                frames.Add(s);
            else if (f > 1)
                break;
        }

        if (frames.Count == 0)
        {
            SetIdle();
            yield break;
        }

        foreach (var s in frames)
        {
            castorImage.sprite = s;
            yield return new WaitForSeconds(frameDelay);
        }

        SetIdle();
    }

    private bool TryGetSprite(CastorMood mood, int frame, Outfit outfit, out Sprite sprite)
        => map.TryGetValue((mood, frame, outfit), out sprite);

    private bool TryGetAnyFrame(CastorMood mood, Outfit outfit, out Sprite sprite)
    {
        for (int f = 1; f <= 20; f++)
            if (TryGetSprite(mood, f, outfit, out sprite))
                return true;
        sprite = null;
        return false;
    }

    private void BuildMapFromLibrary()
    {
        map.Clear();

        if (library == null)
        {
            Debug.LogError("CastorController: Assign a CastorSpriteLibrary in the Inspector.");
            return;
        }

        foreach (var s in library.sprites)
        {
            if (s == null) continue;
            if (TryParse(s.name, out var mood, out var frame, out var outfit))
                map[(mood, frame, outfit)] = s;
        }

        Debug.Log($"Castor sprites indexed from library: {map.Count}");
    }

    private bool TryParse(string name, out CastorMood mood, out int frame, out Outfit outfit)
    {
        mood = CastorMood.Normal;
        frame = 1;
        outfit = Outfit.None;

        if (!name.StartsWith("castor_")) return false;

        var parts = name.Substring("castor_".Length).Split('_');
        if (parts.Length == 0) return false;

        mood = parts[0] switch
        {
            "normal" => CastorMood.Normal,
            "happy" => CastorMood.Happy,
            "sad" => CastorMood.Sad,
            _ => CastorMood.Normal
        };
        //if (parts[0] is not ("normal" or "happy" or "sad")) return false;

        int i = 1;

        if (i < parts.Length && int.TryParse(parts[i], out int parsedFrame))
        {
            frame = parsedFrame;
            i++;
        }

        for (; i < parts.Length; i++)
        {
            outfit |= parts[i] switch
            {
                "manteau" => Outfit.Manteau,
                "salopette" => Outfit.Salopette,
                "cache" => Outfit.Cache,
                "gants" => Outfit.Gants,
                "tuque" => Outfit.Tuque,
                _ => Outfit.None
            };
        }

        return true;
    }
}