#include "win32wrapper.hpp"

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	win32wrapper win32("my window", hInstance, nCmdShow, 640, 480);
	
	win32.mainLoop();

	return win32.getMessage().wParam;
}