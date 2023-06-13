import { shallowMount, createLocalVue } from '@vue/test-utils'
import UserManagement from '@/views/UserManagementView'
import expect from 'expect'
import * as jest from 'node/test'
import { afterEach, beforeEach, describe, it } from 'node:test'

const localVue = createLocalVue()

describe('UserManagement', () => {
  let wrapper

  beforeEach(() => {
    wrapper = shallowMount(UserManagement, {
      localVue
    })
  })

  afterEach(() => {
    wrapper.destroy()
  })

  it('should initialize with the correct data', () => {
    expect(wrapper.vm.addDialogOpen).toBe(false)
    expect(wrapper.vm.editingRows).toEqual([])
    expect(wrapper.vm.userList).toEqual([])
    expect(wrapper.vm.columns).toEqual([
      { field: 'username', header: 'Korisničko ime' },
      { field: 'firstName', header: 'Ime' },
      { field: 'lastName', header: 'Prezime' },
      { field: 'roleId', header: 'Uloga' }
    ])
  })

  it('should toggle the dialog when `toggleDialog` method is called', () => {
    expect(wrapper.vm.addDialogOpen).toBe(false)

    wrapper.vm.toggleDialog()

    expect(wrapper.vm.addDialogOpen).toBe(true)

    wrapper.vm.toggleDialog()

    expect(wrapper.vm.addDialogOpen).toBe(false)
  })

  it('should return the correct role based on the roleId when `getRole` method is called', () => {
    const allRoles = [
      { id: 1, name: 'Admin' },
      { id: 2, name: 'Mentor' },
      { id: 3, name: 'Student' }
    ]

    wrapper.setData({ allRoles })

    const roleId = 2
    const expectedRole = { id: 2, name: 'Mentor' }

    const role = wrapper.vm.getRole(roleId)

    expect(role).toEqual(expectedRole)
  })

  it('should return the correct roles when `roles` method is called', () => {
    const expectedRoles = [
      { id: 1, name: 'Admin' },
      { id: 2, name: 'Mentor' },
      { id: 3, name: 'Student' }
    ]

    const roles = wrapper.vm.roles()

    expect(roles).toEqual(expectedRoles)
  })

  it('should update the user and display success toast when `onRowEditSave` method is called', async () => {
    const mockResponse = {
      data: {
        id: 1,
        username: 'eKrulcic',
        firstName: 'Emil',
        lastName: 'Krulcic',
        roleId: 1
      }
    }

    const mockEvent = {
      data: {
        id: 1,
        username: 'eKrulcic',
        firstName: 'Emil',
        lastName: 'Krulcic',
        roleId: 1
      },
      newData: {
        id: 2,
        username: 'pBaldas',
        firstName: 'Paolo',
        lastName: 'Baldas',
        roleId: 2
      }
    }

    const userController = {
      updateUser: jest.fn().mockResolvedValue(mockResponse)
    }

    wrapper.vm.userController = userController
    wrapper.vm.userList = [mockEvent.data]

    await wrapper.vm.onRowEditSave(mockEvent)

    expect(userController.updateUser).toHaveBeenCalledWith(mockEvent.newData)
    expect(wrapper.vm.userList).toEqual([mockEvent.newData])
    expect(wrapper.vm.$toast.add).toHaveBeenCalledWith({
      severity: 'success',
      summary: 'Uspješno',
      detail: 'Korisnik je uspješno ažuriran',
      life: 3000
    })
  })

  it('should fetch all users when `getAllUsers` method is called', async () => {
    const mockResponse = {
      data: [
        { id: 1, username: 'eKrulcic', firstName: 'Emil', lastName: 'Krulcic', roleId: 1 },
        { id: 2, username: 'pBaldas', firstName: 'Paolo', lastName: 'Baldas', roleId: 2 }
      ]
    }

    const userController = {
      getAllUsers: jest.fn().mockResolvedValue(mockResponse)
    }

    wrapper.vm.userController = userController

    await wrapper.vm.getAllUsers()

    expect(userController.getAllUsers).toHaveBeenCalled()
    expect(wrapper.vm.userList).toEqual(mockResponse.data)
  })

  it('should set `addDialogOpen` to true when `addUser` method is called', () => {
    expect(wrapper.vm.addDialogOpen).toBe(false)

    wrapper.vm.addUser()

    expect(wrapper.vm.addDialogOpen).toBe(true)
  })
})
