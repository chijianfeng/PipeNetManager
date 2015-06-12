// acceleratelib.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <amp.h>
using namespace concurrency;

/*
判断检查井是否在当前的显示范围中

*/
extern "C" __declspec(dllexport) int __stdcall Acc_InRange(float StartX, float StartY, float EndX, float EndY,
	float* pJuncx, float* pJuncY, int datalen, int* Indexs)
{
	return 0;
}
/*
计算检查井的位置，Win7下，由于AMP只支持float，所以传人参数为float
pJuncx：检查井的x坐标
pJuncy: 检查井的y坐标
datalen：传人数据的长度

*/
extern "C" __declspec (dllexport) void __stdcall Cal_JuncPosition(float* pJuncsx,float* pJuncsy, int datalen , 
	float X , float Y ,float DX , float DY)
{
	if (!pJuncsx||!pJuncsy)
		return;
	array_view<float, 1> Junc_X(datalen, pJuncsx);				//将数据输入显存
	array_view<float, 1> Junc_Y(datalen, pJuncsy);

	parallel_for_each(Junc_X.extent, [=](index<1> idx) restrict(amp)
	{
		//add code here
		Junc_X[idx] = (Junc_X[idx] - X) / DX;
		Junc_Y[idx] = (Y - Junc_Y[idx]) / DY;
	});
	Junc_X.synchronize();									//写回内存
	Junc_Y.synchronize();
}

/*
计算管道的位置
pX：起始点
pY：终止点
datalen：数据长度
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

