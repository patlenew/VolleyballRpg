using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using Unity.Burst;

public class Texture3DGenerator : MonoBehaviour
{
    public int3 sizes;
    public Noise[] noises;
    
    [ContextMenu("Generate Texture")]
    public Texture2D GenerateTexture2D()
    {
        Texture2D texture = new Texture2D(sizes.x, sizes.y);
        NativeArray<Color> outputs = new NativeArray<Color>(sizes.x * sizes.y, Allocator.TempJob);
        NativeArray<Noise> nativeNoises = new NativeArray<Noise>(noises, Allocator.TempJob);

        new RenderTexture()
        {
            noises = nativeNoises,
            outputs = outputs,
            sizes = new int3(sizes.x, sizes.y, 1)
        }.Schedule(outputs.Length, 64).Complete();

        for (int x = 0; x < sizes.x; x++)
        {
            for (int y = 0; y < sizes.y; y++)
            {
                texture.SetPixel(x, y, outputs[x + y * sizes.x]);
            }
        }

        outputs.Dispose();
        nativeNoises.Dispose();

        return texture;
    }
    [ContextMenu("Generate Texture 3D")]
    public Texture3D GenerateTexture3D()
    {
        Texture3D texture = new Texture3D(sizes.x, sizes.y, sizes.z, TextureFormat.Alpha8, false);
        NativeArray<Color> outputs = new NativeArray<Color>(sizes.x * sizes.y * sizes.z, Allocator.TempJob);
        NativeArray<Noise> nativeNoises = new NativeArray<Noise>(noises, Allocator.TempJob);

        new RenderTexture()
        {
            noises = nativeNoises,
            outputs = outputs,
            sizes = sizes,
            is3D = true
        }.Schedule(outputs.Length, 64).Complete();

        for (int x = 0; x < sizes.x; x++)
        {
            for (int y = 0; y < sizes.y; y++)
            {
                for (int z = 0; z < sizes.z; z++)
                {
                    int index = to1D(new int3(x, y, z), sizes);
                    texture.SetPixel(x,y,z, outputs[index]);
                }
            }
        }
        outputs.Dispose();
        nativeNoises.Dispose();

        return texture;
    }

    [ContextMenu("Generate Texture 32x32x32")]
    public Texture2D GenerateTexture32x32x32()
    {
        int3 sizes = 32;
        Texture2D texture = new Texture2D(sizes.x * sizes.z, sizes.y, TextureFormat.ARGB32, false);
        NativeArray<Color> outputs = new NativeArray<Color>(sizes.x * sizes.y * sizes.z, Allocator.TempJob);
        NativeArray<Noise> nativeNoises = new NativeArray<Noise>(noises, Allocator.TempJob);

        new RenderTexture()
        {
            noises = nativeNoises,
            outputs = outputs,
            sizes = sizes,
            is3D = true
        }.Schedule(outputs.Length, 64).Complete();

        //z is img count
        for (int z = 0; z < sizes.z; z++)
        {
            for (int x = 0; x < sizes.x; x++)
            {
                for (int y = 0; y < sizes.y; y++)
                {
                    int index = to1D(new int3(x, y, z), sizes);
                    texture.SetPixel(x + (sizes.x * z), y, outputs[index]);
                }
            }
        }
        outputs.Dispose();
        nativeNoises.Dispose();

        return texture;
    }


    public enum NoiseType { Worley, CNoise, SNoise}
    
    [System.Serializable]
    public struct Noise
    {
        public float amplitude;
        public float scale;
        public float3 offset;
        public NoiseType type;
        public bool isInverted;

        public float CalculateValue(float3 pos)
        {
            float noiseValue = CalculateNoise(pos * scale + offset);
            //normalize [-1, 1] to [0, 1]
            noiseValue = noiseValue * 0.5f + 0.5f;
            if (isInverted)
                noiseValue = 1 - noiseValue;
            return amplitude * math.saturate(noiseValue);
        }

        float CalculateNoise(float3 pos)
        {
            switch (type)
            {
                case NoiseType.Worley:
                    return noise.cellular(pos).x;
                case NoiseType.CNoise:
                    return noise.cnoise(pos);
                case NoiseType.SNoise:
                    return noise.snoise(pos);
            }
            return 0;
        }
    }

    [BurstCompile]
    struct RenderTexture : IJobParallelFor
    {
        public int3 sizes;
        [ReadOnly] public NativeArray<Noise> noises;
        public NativeArray<Color> outputs;
        public bool is3D;

        public void Execute(int index)
        {
            float3 uv;         
            if(is3D)
                uv = to3D(index, sizes) / sizes;
            else
                uv = new float3(IndexToPos(index, sizes.xy), 0) / sizes;

            float value = 0;
            for (int i = 0; i < noises.Length; i++)
            {
                value += noises[i].CalculateValue(uv);
            }
            //add multiple channels
            value = math.saturate(value);
            outputs[index] = new Color(value, value, value, 1);
        }


        public static int2 IndexToPos(int i, int2 sizes)
        {
            return new int2(i % sizes.x, i / sizes.y);
        }
    }

    public static int to1D(int3 pos, int3 sizes)
    {
        return (pos.z * sizes.x * sizes.y) + (pos.y * sizes.x) + pos.x;
    }

    public static int3 to3D(int index, int3 sizes)
    {
        int x = index % sizes.x;
        int y = (index / sizes.x) % sizes.y;
        int z = index / (sizes.x * sizes.y);
        return new int3(x, y, z);
    }
}
