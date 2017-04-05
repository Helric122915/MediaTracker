using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker.Helper
{
    public enum MediaType
    {
        Movie,
        VideoGame,
        Music
    }

    public enum MovieSort
    {
        Title,
        PersonalRating,
        Director,
        LeastRecentlyUsed,
        MostRecentlyUsed,
        None
    }

    public enum VideoGameSort
    {
        Title,
        PersonalRating,
        ESRB,
        LeastRecentlyUsed,
        MostRecentlyUsed,
        None
    }

    public enum MusicSort
    {
        Title,
        PersonalRating,
        LeastRecentlyUsed,
        MostRecentlyUsed,
        None
    }

    public enum MPAA
    {
        None,
        G,
        PG,
        PG13,
        R,
        NC17
    }

    public enum ESRB
    {
        None,
        RP,
        EC,
        E,
        E10,
        T,
        M,
        AO
    }
}