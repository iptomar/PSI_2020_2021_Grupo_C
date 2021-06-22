import time
import pyautogui
from random import randint
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
fraseSeed = "Viverra mauris in aliquam sem. Adipiscing at in tellus integer feugiat scelerisque varius morbi enim. Tempus egestas sed sed risus pretium. Blandit massa enim nec dui nunc mattis enim ut. Nullam vehicula ipsum a arcu cursus vitae congue. Mattis enim ut tellus elementum sagittis vitae et leo. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras. Viverra justo nec ultrices dui sapien. Aliquam id diam maecenas ultricies mi eget mauris pharetra et. Fringilla ut morbi tincidunt augue interdum. Porttitor massa id neque aliquam vestibulum. Eget dolor morbi non arcu risus quis varius quam quisque. Sed tempus urna et pharetra pharetra massa massa ultricies. Vitae ultricies leo integer malesuada nunc vel risus commodo. Dignissim diam quis enim lobortis scelerisque fermentum dui faucibus in. Ut sem viverra aliquet eget sit. Sagittis aliquam malesuada bibendum arcu vitae. Nisi vitae suscipit tellus mauris a diam maecenas.Diam phasellus vestibulum lorem sed risus ultricies. Erat nam at lectus urna duis convallis convallis tellus. Fermentum odio eu feugiat pretium nibh ipsum consequat nisl. Eu augue ut lectus arcu bibendum. Risus sed vulputate odio ut enim blandit volutpat maecenas volutpat. Etiam non quam lacus suspendisse. Eget egestas purus viverra accumsan in nisl nisi scelerisque eu. Ultrices sagittis orci a scelerisque purus semper eget duis at. Mi sit amet mauris commodo quis imperdiet massa. Nisl condimentum id venenatis a condimentum vitae sapien. At elementum eu facilisis sed odio morbi quis commodo odio. Nisi porta lorem mollis aliquam. Id diam vel quam elementum. Eu volutpat odio facilisis mauris sit amet massa vitae. Tincidunt ornare massa eget egestas purus viverra accumsan."


def gerarFrase(tamanho):
    if(tamanho > len(fraseSeed)): tamanho = len(fraseSeed)
    inicio = randint(0, len(fraseSeed) - tamanho)
    return fraseSeed[inicio: inicio + tamanho]



def contains(array, titulo):
    for i in range(len(array)):
        if(array[i][0] == titulo):return i
    return None


def test(amostra, titulo, descricao, i):
    assert amostra[0] == titulo and (amostra[1] == descricao), "Teste " + str(i) +  " raealizado com insucesso, descricao ou titulo errados: " + titulo + " : " + descricao


def trim(frase):
    while(frase.endswith(" ")):frase = frase[:len(frase) - 1]
    while (frase.startswith(" ")): frase = frase[1:]
    return frase


service = Service('chromedriver.exe')
service.start()
driver = webdriver.Remote(service.service_url)
driver.get('https://luiza-andaluz.azurewebsites.net/');
time.sleep(2)
memoria=[]
for i in range(1):
#   Click on Select Language
    pyautogui.click(1125, 250)
    time.sleep(1)
    
#   Select English Language
    pyautogui.click(1064, 828)
    time.sleep(1)
    
#   Go to Login Page 
    pyautogui.click(1453, 250)
    time.sleep(5)
    
#   Fill Email Area
    pyautogui.click(137, 585)
    time.sleep(1)
    pyautogui.write("bilal.ulutas", interval=0.01)
    #@ Symbol (for Turkish F Keyboard)
    pyautogui.hotkey('altright','q')
    pyautogui.write("hotmail.com", interval=0.01)
    time.sleep(1)    
    
#   Enter Password
    pyautogui.click(137, 723)
    time.sleep(1)
    pyautogui.write("MyPassword2021!", interval=0.01)
    time.sleep(1)
    
#   Click on "Remember Me"
    pyautogui.click(100, 791)
    time.sleep(1)
    
#   Click on Login Button
    pyautogui.click(139, 879)
    time.sleep(3)
    
    print("Logged in Successfully!")
driver.quit()