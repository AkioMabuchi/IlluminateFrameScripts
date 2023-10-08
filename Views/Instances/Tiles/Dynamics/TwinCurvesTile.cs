using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Dynamics
{
    public class TwinCurvesTile : TileBase, IMovableTile, IRenderableTile, IRotatableTile, IThrowableTile
    {
        [SerializeField] private Transform transformRotatable;

        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLineA;
        [SerializeField] private MeshRenderer meshRendererLineB;

        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;

        private RotateStatus _rotateStatus = RotateStatus.Rotate0;
        private ElectricStatus _electricStatusLineA = ElectricStatus.None;
        private ElectricStatus _electricStatusLineB = ElectricStatus.None;

        private Sequence _sequenceRotate;
        private Coroutine _coroutineThrow;
        
        private float _currentRotationY;

        public void SetRotateStatus(RotateStatus rotateStatus)
        {
            _rotateStatus = rotateStatus;
        }

        public void SetElectricStatusLineA(ElectricStatus electricStatus)
        {
            _electricStatusLineA = electricStatus;
        }

        public void SetElectricStatusLineB(ElectricStatus electricStatus)
        {
            _electricStatusLineB = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLineA.enabled = true;
            meshRendererLineB.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLineA.enabled = false;
            meshRendererLineB.enabled = false;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void Render()
        {
            meshRendererLineA.material = electricMaterials.GetElectricMaterial(_electricStatusLineA);
            meshRendererLineB.material = electricMaterials.GetElectricMaterial(_electricStatusLineB);
        }

        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLineA.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineA);
                            break;
                        }
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLineB.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineB);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLineA.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineA);
                            break;
                        }
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLineB.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineB);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLineA.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineA);
                            break;
                        }
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLineB.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineB);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLineA.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineA);
                            break;
                        }
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLineB.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLineB);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses)
        {
            var hashSetElectricStatuses = new HashSet<ElectricStatus>(electricStatuses);

            if (hashSetElectricStatuses.Contains(_electricStatusLineA))
            {
                meshRendererLineA.material = electricMaterials.None;
            }

            if (hashSetElectricStatuses.Contains(_electricStatusLineB))
            {
                meshRendererLineB.material = electricMaterials.None;
            }
        }

        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLineA.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLineB.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLineA.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLineB.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLineA.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLineB.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLineA.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLineB.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void RenderReset()
        {
            meshRendererLineA.material = electricMaterials.None;
            meshRendererLineB.material = electricMaterials.None;
        }

        public void Rotate()
        {
            _sequenceRotate?.Kill();

            var targetRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            while (_currentRotationY > targetRotationY)
            {
                targetRotationY += 360.0f;
            }

            _sequenceRotate = DOTween.Sequence()
                .Append(DOTween
                    .To(() => _currentRotationY, value => { _currentRotationY = value; }, targetRotationY, 0.2f)
                    .SetEase(Ease.OutCubic))
                .OnUpdate(() => { transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f); })
                .OnComplete(() =>
                {
                    _currentRotationY = _rotateStatus switch
                    {
                        RotateStatus.Rotate0 => 0.0f,
                        RotateStatus.Rotate90 => 90.0f,
                        RotateStatus.Rotate180 => 180.0f,
                        RotateStatus.Rotate270 => 270.0f,
                        _ => throw new Exception()
                    };

                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                }).SetLink(gameObject);
        }

        public void RotateImmediate()
        {
            _sequenceRotate?.Kill();

            _currentRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
        }

        public void RotateReset()
        {
            _sequenceRotate?.Kill();
            _rotateStatus = RotateStatus.Rotate0;
            _currentRotationY = 0.0f;
            transformRotatable.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Throw(Vector3 velocity, Vector3 angularVelocity)
        {
            IEnumerator Coroutine()
            {
                for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
                {
                    var position = transform.position;
                    position += velocity * Time.deltaTime;
                    transform.position = position;

                    var eulerAngles = transform.eulerAngles;
                    eulerAngles += angularVelocity * Time.deltaTime;
                    transform.eulerAngles = eulerAngles;
                    
                    yield return null;
                }
            }

            _coroutineThrow = StartCoroutine(Coroutine());
        }

        public void ThrowReset()
        {
            if (_coroutineThrow != null)
            {
                StopCoroutine(_coroutineThrow);
            }
            
            transform.eulerAngles = Vector3.zero;
        }
    }
}
