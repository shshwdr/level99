using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class FMODEvents : Singleton<FMODEvents>
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference music_main_menu { get; private set; }
    [field: SerializeField] public List<EventReference> musicPlaylist { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference submerge { get; private set; }
    [field: SerializeField] public EventReference emerge { get; private set; }
    [field: SerializeField] public EventReference swim_bubbles { get; private set; }
    //[field: SerializeField] public EventReference ui_click { get; private set; }

    private int nextPlaylistIndex = 0;

    internal EventReference getNextMusicReference()
    {
        if (nextPlaylistIndex >= musicPlaylist.Count - 1)
        {
            nextPlaylistIndex = 0;
        }
        EventReference reference = musicPlaylist[nextPlaylistIndex];
        nextPlaylistIndex++;
        return musicPlaylist[nextPlaylistIndex];
    }
}
