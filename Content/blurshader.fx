#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_2_0
	#define PS_SHADERMODEL ps_2_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float BlurIntensity = 0.007f;

SamplerState SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 Tex : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 outputColor;

	outputColor =  SpriteTexture.Sample(SpriteTextureSampler, float2(input.Tex.x + BlurIntensity, input.Tex.y + BlurIntensity));
	outputColor += SpriteTexture.Sample(SpriteTextureSampler, float2(input.Tex.x - BlurIntensity, input.Tex.y - BlurIntensity));
	outputColor += SpriteTexture.Sample(SpriteTextureSampler, float2(input.Tex.x + BlurIntensity, input.Tex.y - BlurIntensity));
	outputColor += SpriteTexture.Sample(SpriteTextureSampler, float2(input.Tex.x - BlurIntensity, input.Tex.y + BlurIntensity));

	outputColor = outputColor / 4;
	return outputColor;
}

technique SpriteDrawing
{
	pass P0
	{
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};

