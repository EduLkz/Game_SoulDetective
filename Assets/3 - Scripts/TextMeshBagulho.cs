using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshBagulho : MonoBehaviour {
    TMP_Text textMesh;
    Mesh mesh;
    Vector3[] vertices;

    public Vector2 bla;

    private void Start() {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update() {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for(int i = 0; i < vertices.Length; i++) {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float _time) {
        return new Vector2(Mathf.Sin(_time * bla.x), Mathf.Cos(_time * bla.y));
    }
}