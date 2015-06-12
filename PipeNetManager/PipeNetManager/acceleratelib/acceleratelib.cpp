// acceleratelib.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"
#include <amp.h>
using namespace concurrency;

/*
�жϼ�龮�Ƿ��ڵ�ǰ����ʾ��Χ��

*/
extern "C" __declspec(dllexport) int __stdcall Acc_InRange(float StartX, float StartY, float EndX, float EndY,
	float* pJuncx, float* pJuncY, int datalen, int* Indexs)
{
	return 0;
}
/*
�����龮��λ�ã�Win7�£�����AMPֻ֧��float�����Դ��˲���Ϊfloat
pJuncx����龮��x����
pJuncy: ��龮��y����
datalen���������ݵĳ���

*/
extern "C" __declspec (dllexport) void __stdcall Cal_JuncPosition(float* pJuncsx,float* pJuncsy, int datalen , 
	float X , float Y ,float DX , float DY)
{
	if (!pJuncsx||!pJuncsy)
		return;
	array_view<float, 1> Junc_X(datalen, pJuncsx);				//�����������Դ�
	array_view<float, 1> Junc_Y(datalen, pJuncsy);

	parallel_for_each(Junc_X.extent, [=](index<1> idx) restrict(amp)
	{
		//add code here
		Junc_X[idx] = (Junc_X[idx] - X) / DX;
		Junc_Y[idx] = (Y - Junc_Y[idx]) / DY;
	});
	Junc_X.synchronize();									//д���ڴ�
	Junc_Y.synchronize();
}

/*
����ܵ���λ��
pX����ʼ��
pY����ֹ��
datalen�����ݳ���
*/
extern "C" __declspec (dllexport) void __stdcall Cal_PipePosition(float* pStartX, float* pStartY, float* pEndX
	,float* pEndY , int datalen, float X ,float Y , float DX , float DY)
{
	if (!pStartX || !pStartY || !pEndX||!pEndY)
		return;

	array_view<float, 1> StartX(datalen, pStartX);
	array_view<float, 1> StartY(datalen, pStartY);
	array_view<float, 1> EndX(datalen, pEndX);
	array_view<float, 1> EndY(datalen, pEndY);

	parallel_for_each(StartX.extent, [=](index<1> idx)restrict(amp)
	{
		StartX[idx] = (StartX[idx] - X) / DX;
		StartY[idx] = (Y - StartY[idx]) / DY;

		EndX[idx] = (EndX[idx] - X) / DX;
		EndY[idx] = (Y - EndY[idx]) / DY;
	});

	StartX.synchronize();
	StartY.synchronize();
	EndX.synchronize();
	EndY.synchronize();
}

