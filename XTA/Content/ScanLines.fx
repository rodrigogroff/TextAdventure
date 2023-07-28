
// ScanLines.fx

sampler2D SceneSampler : register(s0);

// Effect parameters
float ScanLineIntensity : register(c0);
float ScanLineFrequency : register(c1);

// The pixel shader
float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    // Calculate scan line position based on the Y coordinate
    float scanLinePos = (texCoord.y * ScanLineFrequency);

    // Create the scan lines effect
    float scanLine = (sin(scanLinePos) + 1.0) * 0.5;
    float4 color = tex2D(SceneSampler, texCoord);
    color.rgb *= (1.0 - (scanLine * ScanLineIntensity));

    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}

