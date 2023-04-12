using UnityEngine;
public static class EdgeWrapSystem
{

    //Imitating the screen wrap effect
    public static Vector3 screenWrapPosition(Vector3 position)
    {
        byte areainfo = PlayArea.instance.ObjectWithinBoundsInfo(position);
        //Debug.Log(areainfo);
        Vector3 screenwrapposition = position;
        if ((areainfo & 0b0001) == 0b0001) screenwrapposition.x = position.x - PlayArea.instance.bounds.width;
        if ((areainfo & 0b0010) == 0b0010) screenwrapposition.x = position.x + PlayArea.instance.bounds.width;
        if ((areainfo & 0b0100) == 0b0100) screenwrapposition.y = position.y - PlayArea.instance.bounds.height;
        if ((areainfo & 0b1000) == 0b1000) screenwrapposition.y = position.y + PlayArea.instance.bounds.height;

        return screenwrapposition;
    }
}