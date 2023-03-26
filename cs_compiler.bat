@echo off
title C# Compiler

set /P filename=File Name: 

%CS_COMPILER% /t:exe /out:%filename%.exe %filename%.cs
pause