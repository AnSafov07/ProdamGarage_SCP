using UnityEngine;

namespace CustomizationSL
{
    public class SchematicData
    {
        public float x { get; set; } = 0f;
        public float y { get; set; } = 0f;
        public float z { get; set; } = 0f;

        public float rx { get; set; } = 0f;
        public float ry { get; set; } = 0f;
        public float rz { get; set; } = 0f;

        public bool? visible_schem { get; set; } = null;
        public bool? visible_player { get; set; } = true;
        public int rank { get; set; } = 1;

        public Vector3 GetOffset() => new Vector3(x, y, z);
        public Quaternion GetRotation() => Quaternion.Euler(rx, ry, rz);
    }
}
